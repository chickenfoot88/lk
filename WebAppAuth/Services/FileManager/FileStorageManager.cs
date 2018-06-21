using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using OhDataManager.Entities.System;
using OhDataManager.Enums;
using OhUtils;
using WebAppAuth.Context;

namespace WebAppAuth.Services.FileManager
{
    /// <summary>
    /// Файловый менеджер
    /// </summary>
    public class FileStorageManager : IFileStorageManager
    {
        private readonly string _fileStorage;

        /// <summary>
        /// Конструктор
        /// </summary>
        public FileStorageManager()
        {
            //из конфига получаем корневой путь файлового хранилища
            _fileStorage = ConfigurationManager.AppSettings.Get("FileStorage");
            if (string.IsNullOrEmpty(_fileStorage))
                throw new Exception("В конфигурационном файле не найдено значение для параметра 'FileStorage'!");
        }

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="userIdentityid"></param>
        /// <returns></returns>
        private async Task<long> SaveFile(Stream stream, string fileName, int userIdentityid)
        {
            if (stream == null)
                throw new Exception("Входной поток не определен");

            //проверяем директорию на существование
            //в случае отсутствия - создаем
            if (!Directory.Exists(_fileStorage))
                Directory.CreateDirectory(_fileStorage);

            using (var context = new ApplicationDbContext())
            {
                var storedName = DateTime.Now.GetHashCode().ToString("x") + Path.GetExtension(fileName);

                //создаем сущность файла
                var exchangeFile = new ExchangeFile
                {
                    UserIdentityId = userIdentityid,
                    FileName = fileName,
                    StoredName = storedName,
                    StartTime = DateTime.Now,
                    Progress = 0.01m,
                    Organization =
                        context.Employee.Where(x => x.ApplicationUser.Id == userIdentityid)
                            .Select(x => x.Organization)
                            .FirstOrDefault() ??
                            context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid)
                            .Select(x => x.Organization)
                            .FirstOrDefault()
                };
                context.ExchangeFiles.Add(exchangeFile);
                await context.SaveChangesAsync();

                //названием файла на сервере будет "идентификатор файла"."расширение файла" в БД
                //сохраняем файл в файловое хранилище
                using (
                    var outStream = new FileStream(Path.Combine(_fileStorage, storedName), FileMode.CreateNew,
                        FileAccess.Write))
                {
                    stream.CopyTo(outStream);
                    outStream.Flush();
                    outStream.Close();
                    stream.Close();
                    stream.Dispose();
                }

                return exchangeFile.Id;
            }
        }


        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="file">Загружаемый файл</param>
        /// <param name="userIdentityid"></param>
        /// <param name="loadType"></param>
        /// <returns>Идентификатор файла в БД</returns>
        public async Task<ImportedFile> SaveImportedFile(HttpPostedFileBase file, int userIdentityid,
            EImportType loadType)
        {
            if (file == null)
                throw new Exception("Входной файл не определен");

            var exchangeFileId = await SaveFile(file.InputStream, file.FileName, userIdentityid);

            using (var context = new ApplicationDbContext())
            {
                //создаем сущность импортированного файла
                var importedFile = new ImportedFile
                {
                    LoadType = loadType,
                    LoadStatus = ELoadStatus.InProcess,
                    ExchangeFile = await context.ExchangeFiles.FindAsync(exchangeFileId),
                    Organization =
                        context.Employee.Where(x => x.ApplicationUser.Id == userIdentityid)
                            .Select(x => x.Organization)
                            .FirstOrDefault() ??
                            context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid)
                            .Select(x => x.Organization)
                            .FirstOrDefault()
                };
                context.ImportedFiles.Add(importedFile);
                await context.SaveChangesAsync();

                return importedFile;
            }
        }

        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="userIdentityid"></param>
        /// <param name="exportType"></param>
        /// <returns></returns>
        public async Task<ExportedFile> SaveExportedFile(Stream stream, string fileName, int userIdentityid,
            EExportType exportType)
        {
            if (stream == null)
                throw new Exception("Входной файл не определен");

            var exchangeFileId = await SaveFile(stream, fileName, userIdentityid);

            using (var context = new ApplicationDbContext())
            {
                //создам сущность экспортируемого файла
                var exportedFile = new ExportedFile
                {
                    UnloadType = exportType,
                    UnloadStatus = EUnloadStatus.InProcess,
                    ExchangeFile = await context.ExchangeFiles.FindAsync(exchangeFileId),
                    Organization =
                        context.Employee.Where(x => x.ApplicationUser.Id == userIdentityid)
                            .Select(x => x.Organization)
                            .FirstOrDefault()
                };
                context.ExportedFiles.Add(exportedFile);
                await context.SaveChangesAsync();

                return exportedFile;
            }
        }

        /// <summary>
        /// Обновить файл
        /// </summary>
        /// <param name="exchangeFileId"></param>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="userIdentityid"></param>
        /// <returns></returns>
        public void UpdateFile(long exchangeFileId, Stream stream, string fileName, int userIdentityid)
        {
            if (stream == null)
                throw new Exception("Входной поток не определен");

            //проверяем директорию на существование
            //в случае отсутствия - создаем
            if (!Directory.Exists(_fileStorage))
                Directory.CreateDirectory(_fileStorage);

            using (var context = new ApplicationDbContext())
            {
                var exchangeFile = context.ExchangeFiles.Find(exchangeFileId);
                if (exchangeFile==null)
                    throw new FileNotFoundException("Не найден файл для идентификатора " + exchangeFileId);

                var oldStoredName = exchangeFile.StoredName;
                var newStoredName = DateTime.Now.GetHashCode().ToString("x") + Path.GetExtension(fileName);

                //названием файла на сервере будет "идентификатор файла"."расширение файла" в БД
                //сохраняем файл в файловое хранилище
                using (
                    var outStream = new FileStream(Path.Combine(_fileStorage, newStoredName), FileMode.CreateNew,
                        FileAccess.Write))
                {
                    stream.CopyTo(outStream);
                    outStream.Flush();
                    outStream.Close();
                    stream.Close();
                    stream.Dispose();
                }
                exchangeFile.FileName = fileName;
                exchangeFile.StoredName = newStoredName;
                context.SaveChangesAsync();

                try
                {
                    File.Delete(Path.Combine(_fileStorage, oldStoredName));
                }
                catch (Exception ex)
                {
                    LogUtils.WriteException(ex, LogUtils.ELogType.Warn,
                        "Ошибка при удалении файла после обновления");
                }
            }
        }

        /// <summary>
        /// Получение файла по идентификатору
        /// </summary>
        /// <param name="exchangeFileId"></param>
        /// <returns></returns>
        public async Task<Stream> GetFileStream(long exchangeFileId)
        {
            using (var context = new ApplicationDbContext())
            {
                var exchangeFile = await context.ExchangeFiles.FindAsync(exchangeFileId);

                if (exchangeFile == null)
                    throw new FileNotFoundException("Не найден файл для идентификатора " + exchangeFileId);

                return
                    new FileStream(Path.Combine(_fileStorage, exchangeFile.StoredName),
                        FileMode.Open, FileAccess.Read);
            }
        }

        /// <summary>
        /// Получение файла по идентификатору
        /// </summary>
        /// <param name="exchangeFileId"></param>
        /// <returns></returns>
        public async Task<byte[]> GetFileBytes(long exchangeFileId)
        {
            using (var stream = await GetFileStream(exchangeFileId))
            using (var memStream = new MemoryStream())
            {
                stream.CopyTo(memStream);
                return memStream.ToArray();
            }
        }
    }
}
