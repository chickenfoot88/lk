using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using OhDataManager.Enums;
using OhUtils;
using WebAppAuth.Context;
using WebAppAuth.Services.FileManager;

namespace WebAppAuth.Services.Import
{
    /// <summary>
    /// Базовый класс загрузки данных
    /// </summary>
    public abstract class BaseFileImport
    {
        /// <summary>
        /// Тип загрузки
        /// </summary>
        public virtual EImportType LoadType { get; set; }

        /// <summary>
        /// Менеджер файлового хранилища
        /// </summary>
        public IFileStorageManager FileStorageManager { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fileStorageManager"></param>
        protected BaseFileImport(IFileStorageManager fileStorageManager)
        {
            FileStorageManager = fileStorageManager;
        }

        /// <summary>
        /// Запуск импорта
        /// </summary>
        /// <returns></returns>
        public async Task Run(HttpPostedFileBase inputFile, IPrincipal principal)
        {
            try
            {
                var userIdentityid = principal.Identity.GetUserId<int>();

                var importedFile = await FileStorageManager.SaveImportedFile(inputFile, userIdentityid, this.LoadType);
                var stream = await FileStorageManager.GetFileStream(importedFile.ExchangeFileId);

                Task.Factory.StartNew(
                    () =>
                    {
                        try
                        {
                            RunImport(stream).Wait();
                            using (var context = new ApplicationDbContext())
                            {
                                context.ImportedFiles.Find(importedFile.Id).LoadStatus = ELoadStatus.Success;
                                var file = context.ExchangeFiles.Find(importedFile.ExchangeFileId);
                                file.FinishTime = DateTime.Now;
                                file.Progress = 1m;
                                file.Organization =
                                    context.Employee.Where(x => x.ApplicationUser.Id == userIdentityid)
                                        .Select(x => x.Organization)
                                        .FirstOrDefault();
                                context.SaveChangesAsync();
                            }
                        }
                        catch (Exception ex)
                        {
                            using (var context = new ApplicationDbContext())
                            {
                                context.ImportedFiles.Find(importedFile.Id).LoadStatus = ELoadStatus.DataError;
                                var file = context.ExchangeFiles.Find(importedFile.ExchangeFileId);
                                file.FinishTime = DateTime.Now;
                                file.Organization =
                                    context.Employee.Where(x => x.ApplicationUser.Id == userIdentityid)
                                        .Select(x => x.Organization)
                                        .FirstOrDefault();
                                context.SaveChangesAsync();
                            }

                            LogUtils.WriteException(ex, LogUtils.ELogType.Error, $"Ошибка при импорте '{LoadType.GetDisplayName()}'");
                            throw;
                        }
                    }
                    )
                    .ConfigureAwait(false)
                    .GetAwaiter();
            }
            catch (DbEntityValidationException ex)
            {
                var fullErrorMessage = string.Join("; ", (from validationResult in ex.EntityValidationErrors
                    let entityName = validationResult.Entry.Entity.GetType().Name
                    from error in validationResult.ValidationErrors
                    select $@"Сущность: '{entityName}', поле: '{error.PropertyName}', текст ошибки: '{error.ErrorMessage}'")
                    .ToList());

                LogUtils.WriteException(ex, LogUtils.ELogType.Error, fullErrorMessage);
            }
            catch (Exception ex)
            {
                LogUtils.WriteException(ex, LogUtils.ELogType.Error);
            }
        }

        /// <summary>
        /// Импорт данных
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public abstract Task RunImport(Stream inputStream);
    }
}