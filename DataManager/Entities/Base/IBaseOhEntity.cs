using System;
using System.ComponentModel.DataAnnotations;

namespace OhDataManager.Entities.Base
{
    /// <summary>
    /// Интерфейс базовой сущности
    /// </summary>
    public interface IBaseOhEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        long Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        DateTime CreationTime { get; set; }

        /// <summary>
        /// Время изменения
        /// </summary>
        DateTime ChangedTime { get; set; }
    }
}
