using System.IO;
using System.Threading.Tasks;
using System.Web;
using OhDataManager.Entities.System;
using OhDataManager.Enums;

namespace WebAppAuth.Services.FileManager
{
    /// <summary>
    /// Интерфейс файлового менеджера
    /// </summary>
    public interface IFileStorageManager
    {
        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="file">Загружаемый файл</param>
        /// <param name="userIdentityid"></param>
        /// <param name="loadType"></param>
        /// <returns>Идентификатор файла в БД</returns>
        Task<ImportedFile> SaveImportedFile(HttpPostedFileBase file, int userIdentityid, EImportType loadType);

        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="userIdentityid"></param>
        /// <param name="exportType"></param>
        /// <returns></returns>
        Task<ExportedFile> SaveExportedFile(Stream stream, string fileName, int userIdentityid,
            EExportType exportType);

        /// <summary>
        /// Обновить файл
        /// </summary>
        /// <param name="exchangeFileId"></param>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="userIdentityid"></param>
        /// <returns></returns>
        void UpdateFile(long exchangeFileId, Stream stream, string fileName, int userIdentityid);

        /// <summary>
        /// Получение файла по идентификатору
        /// </summary>
        /// <param name="exchangeFileId"></param>
        /// <returns></returns>
        Task<Stream> GetFileStream(long exchangeFileId);

        /// <summary>
        /// Получение файла по идентификатору
        /// </summary>
        /// <param name="exchangeFileId"></param>
        /// <returns></returns>
        Task<byte[]> GetFileBytes(long exchangeFileId);
    }
}
