using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Enums;
using OhDataManager.Extensions;

namespace OhDataManager.Entities.System
{
    /// <summary>
    /// Выгруженный файл
    /// </summary>
    [Table(nameof(ExportedFile), Schema = "main")]
    public class ExportedFile : BaseOhEntity
    {
        /// <summary>
        /// Файл
        /// </summary>
        public virtual ExchangeFile ExchangeFile { get; set; }

        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public virtual long ExchangeFileId { get; set; }

        /// <summary>
        /// Тип выгрузки
        /// </summary>
        public virtual EExportType UnloadType { get; set; }

        /// <summary>
        /// Cтатус выгрузки
        /// </summary>
        public virtual EUnloadStatus UnloadStatus { get; set; }

        /// <summary>
        /// Текстовое наименование статуса выгрузки
        /// </summary>
        [NotMapped]
        public virtual string UnloadStatusText => UnloadStatus.GetDisplayName();

        /// <summary>
        /// Текстовое наименование типа выгрузки
        /// </summary>
        [NotMapped]
        public virtual string UnloadTypeText => UnloadType.GetDisplayName();
    }
}
