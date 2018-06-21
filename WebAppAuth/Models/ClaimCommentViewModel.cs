using System.ComponentModel.DataAnnotations;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.System;
using OhDataManager.Enums;
using WebAppAuth.Entities;

namespace WebAppAuth.Models
{
    /// <summary>
    /// Комментарий к заявке
    /// </summary>
    public class AbonentClaimCommentViewModel : BaseEntity
    {
        /// <summary>
        /// Заявка 
        /// </summary>  
        public AbonentClaim AbonentClaim { get; set; }

        /// <summary>
        /// Заявка 
        /// </summary>
        public long AbonentClaimId { get; set; }

        /// <summary>
        /// Абонент создатель
        /// </summary>
        public Abonent Creator { get; set; }

        /// <summary>
        /// Абонент создатель
        /// </summary>
        public long? CreatorId { get; set; }

        /// <summary>
        /// Сотрудник
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Сотрудник
        /// </summary>
        public long? EmployeeId { get; set; }

        /// <summary>
        /// Комментарий к заявке
        /// </summary>
        [Required]
        public string Comment { get; set; }

        /// <summary>
        /// Загруженный файл
        /// </summary>
        public ImportedFile ImportedFile { get; set; }

        /// <summary>
        /// Загруженный файл
        /// </summary>
        public long? ImportedFileId { get; set; }

        /// <summary>
        /// Источник информации
        /// </summary>
        public EInformationSource? InformationSource { get; set; }

        /// <summary>
        /// Дата создания комментария в текстовом виде
        /// </summary>
        public string CreationTimeString => CreationTime.ToString("dd-MM-yyyy (HH:mm:ss)");
    }
}
