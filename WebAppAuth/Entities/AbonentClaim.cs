using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Main;
using OhDataManager.Entities.System;
using OhDataManager.Enums;

namespace WebAppAuth.Entities
{
    /// <summary>
    /// Заявка
    /// </summary>
    [Table(nameof(AbonentClaim), Schema = "main")]
    public class AbonentClaim : BaseOhEntity
    {
        /// <summary>
        /// Абонент создатель
        /// </summary>
        public Abonent Creator { get; set; }

        /// <summary>
        /// Абонент создатель
        /// </summary>
        public long? CreatorId { get; set; }
        /// <summary>
        /// Сотрудник создатель
        /// </summary>
        public Employee EmployeeCreator { get; set; }

        /// <summary>
        /// Сотрудник создатель
        /// </summary>
        public long? EmployeeCreatorId { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        public virtual PersonalAccount PersonalAccount { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        public virtual long PersonalAccountId { get; set; }

        /// <summary>
        /// Полное наименование адреса (до комнаты)
        /// </summary>
        [Required]
        public string ApartmentFullAddress { get; set; }
        
        /// <summary>
        /// Вид заявки
        /// </summary>
        public virtual DictClaimKind DictClaimKind { get; set; }

        /// <summary>
        /// Вид заявки
        /// </summary>
        public virtual long DictClaimKindId { get; set; }

        /// <summary>
        /// Комментарий к заявке
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Контактный телефон автора заявки
        /// </summary>
        [Required]
        public string ContactPhoneNumber { get; set; }

        /// <summary>
        /// Статус заявки
        /// </summary>
        public EClaimStatus ClaimStatus { get; set; }

        /// <summary>
        /// Статус заявки
        /// </summary>
        [NotMapped]
        public string ClaimStatusText => ClaimStatus.GetDisplayName();
        
        /// <summary>
        /// Сотрудник
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Сотрудник
        /// </summary>
        public long? EmployeeId { get; set; }

        /// <summary>
        /// Загруженный файл
        /// </summary>
        public ImportedFile ImportedFile { get; set; }

        /// <summary>
        /// Загруженный файл
        /// </summary>
        public long? ImportedFileId { get; set; }

        /// <summary>
        /// Заявка принята
        /// </summary>
        public bool Accepted { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// Заявка закрыта сотрудником
        /// </summary>
        public Employee ClosedByEmployee { get; set; }

        /// <summary>
        /// Заявка закрыта сотрудником
        /// </summary>
        public long? ClosedByEmployeeId { get; set; }

        /// <summary>
        /// Дата закрытия заявки
        /// </summary>
        public DateTime? CompleteDate { get; set; }

        /// <summary>
        /// Стоимость заявки
        /// </summary>
        public decimal? Cost { get; set; }
    }
}
