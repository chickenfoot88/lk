using System;
using System.IO;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OhDataManager.Entities.System;
using OhDataManager.Enums;
using OhUtils;
using WebAppAuth.Context;
using WebAppAuth.Services.FileManager;

namespace WebAppAuth.Services.Export
{
    /// <summary>
    /// Базовый класс загрузки данных
    /// </summary>
    public abstract class BaseFileExport
    {
        /// <summary>
        /// Тип выгрузки
        /// </summary>
        public virtual EExportType UnloadType { get; set; }

        /// <summary>
        /// Файловый менеджер
        /// </summary>
        public IFileStorageManager FileStorageManager { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fileStorageManager"></param>
        protected BaseFileExport(IFileStorageManager fileStorageManager)
        {
            FileStorageManager = fileStorageManager;
        }

        /// <summary>
        /// Запуск экспорта
        /// </summary>
        /// <returns></returns>
        public async Task Run(IPrincipal principal)
        {
            try
            {
                var userIdentityid = principal.Identity.GetUserId<int>();

                try
                {
                    var exportedFile = (await
                        FileStorageManager.SaveExportedFile(new MemoryStream(), "empty.txt", userIdentityid, UnloadType));

                    var exportResult = ExportData(userIdentityid, exportedFile.Id).Result;
                    
                    FileStorageManager.UpdateFile(exportedFile.ExchangeFileId, exportResult.Data, exportResult.FileName,
                        userIdentityid);

                    using (var context = new ApplicationDbContext())
                    {
                        context.ExportedFiles.Find(exportedFile.Id).UnloadStatus = exportResult.Status;
                        var file = context.ExchangeFiles.Find(exportedFile.ExchangeFileId);
                        file.FinishTime = DateTime.Now;
                        file.Progress = 1m;

                        await context.SaveChangesAsync();
                    }
                }
                catch
                {
                    using (var context = new ApplicationDbContext())
                    {
                        var unloadedFile = new ExportedFile
                        {
                            UnloadType = UnloadType,
                            UnloadStatus = EUnloadStatus.UnloadError,
                            ExchangeFile = null
                        };

                        context.ExportedFiles.Add(unloadedFile);
                        await context.SaveChangesAsync();
                    }
                    throw;
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteException(ex, LogUtils.ELogType.Error);
            }
        }

        /// <summary>
        /// Экспорт данных
        /// </summary>
        /// <returns></returns>
        public abstract Task<ExportResult> ExportData(int userIdentityId, long exportedFileId);
    }
}