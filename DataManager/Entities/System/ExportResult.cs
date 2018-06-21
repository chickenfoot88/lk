using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using OhDataManager.Enums;

namespace OhDataManager.Entities.System
{
    /// <summary>
    /// Сущность результата экспорта
    /// </summary>
    [NotMapped]
    public class ExportResult
    {
        /// <summary>
        /// Данные
        /// </summary>
        public Stream Data { get; set; }

        /// <summary>
        /// Лог файл
        /// </summary>
        public Stream Log { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Статус экспорта
        /// </summary>
        public EUnloadStatus Status { get; set; }

        /// <summary>
        /// Успешность экспорта
        /// true : успех, false : неудача
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Наименование файла с расширением
        /// </summary>
        public string FileName { get; set; }
    }
}
