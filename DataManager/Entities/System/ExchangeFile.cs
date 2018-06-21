using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.System
{
    /// <summary>
    /// Файл
    /// </summary>
    [Table(nameof(ExchangeFile), Schema = "main")]
    public class ExchangeFile : BaseOhEntity
    {
        /// <summary>
        /// Загрузивший пользователь
        /// </summary>
        public virtual int UserIdentityId { get; set; }

        /// <summary>
        /// Наименование файла
        /// </summary>
        public virtual string FileName { get; set; }

        /// <summary>
        /// Уникальное имя для хранения
        /// </summary>
        public virtual string StoredName { get; set; }

        /// <summary>
        /// Прогресс выполнения
        /// </summary>
        public virtual decimal Progress { get; set; }
        
        /// <summary>
        /// Время начала загрузки
        /// </summary>
        public virtual DateTime StartTime { get; set; }

        /// <summary>
        /// Время окончания загрузки
        /// </summary>
        public virtual DateTime? FinishTime { get; set; }
    }
}
