using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Enums;
using OhDataManager.Extensions;

namespace OhDataManager.Entities.System
{
    /// <summary>
    /// Загруженный файл
    /// </summary>
    [Table(nameof(ImportedFile), Schema = "main")]
    public class ImportedFile : BaseOhEntity
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
        /// Тип загрузки
        /// </summary>
        public virtual EImportType LoadType { get; set; }

        /// <summary>
        /// Cтатус загрузки
        /// </summary>
        public virtual ELoadStatus LoadStatus { get; set; }

        /// <summary>
        /// Текстовое наименование статуса загрузки
        /// </summary>
        [NotMapped]
        public virtual string LoadStatusText => LoadStatus.GetDisplayName();

        /// <summary>
        /// Текстовое наименование типа загрузки
        /// </summary>
        [NotMapped]
        public virtual string LoadTypeText => LoadType.GetDisplayName();
    }
}
