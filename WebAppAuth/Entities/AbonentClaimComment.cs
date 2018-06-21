using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.System;
using OhDataManager.Enums;

namespace WebAppAuth.Entities
{
    /// <summary>
    /// Комментарий к заявке
    /// </summary>
    [Table(nameof(AbonentClaimComment), Schema = "main")]
    public class AbonentClaimComment : BaseOhEntity
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
    }
}
