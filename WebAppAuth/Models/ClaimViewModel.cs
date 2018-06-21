using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Main;
using OhDataManager.Entities.System;
using OhDataManager.Enums;
using WebAppAuth.Entities;

namespace WebAppAuth.Models
{
    /// <summary>
    /// Заявка
    /// </summary>
    public class ClaimViewModel : BaseEntity
    {
        /// <summary>
        /// Полное наименование адреса (до комнаты)
        /// </summary>
        [Display(Name = "Адрес")]
        public string ApartmentFullAddress { get; set; }

        /// <summary>
        /// Вид заявки
        /// </summary>
        [Display(Name = "Вид заявки")]
        public virtual DictClaimKind DictClaimKind { get; set; }

        /// <summary>
        /// Вид заявки
        /// </summary>
        [Required(ErrorMessage = "Выберите вид заявки")]
        [Display(Name = "Вид заявки")]
        public virtual long DictClaimKindId { get; set; }

        /// <summary>
        /// Вид заявки
        /// </summary>
        [Display(Name = "Вид заявки")]
        public virtual string DictClaimKindName { get; set; }

        /// <summary>
        /// Описание заявки
        /// </summary>
        [Required(ErrorMessage = "Введите описание заявки")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        /// <summary>
        /// Контактный телефон автора заявки
        /// </summary>
        [Required(ErrorMessage = "Введите контактный номер телефона")]
        [Display(Name = "Контактный телефон")]
        public string ContactPhoneNumber { get; set; }

        /// <summary>
        /// Статус заявки
        /// </summary>
        [Display(Name = "Статус заявки")]
        public EClaimStatus ClaimStatus { get; set; }

        /// <summary>
        /// Статус заявки
        /// </summary>
        [Display(Name = "Статус заявки")]
        public string ClaimStatusText => ClaimStatus.GetDisplayName();

        /// <summary>
        /// Дата закрытия заявки
        /// </summary>
        [Display(Name = "Дата закрытия заявки")]
        public DateTime? CompleteDate { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        [Display(Name = "Исполнитель")]
        public Employee Employee { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        [Display(Name = "Исполнитель")]
        public long? EmployeeId { get; set; }

        /// <summary>
        /// Загруженный файл
        /// </summary>
        [Display(Name = "Вложенный файл")]
        public ImportedFile ImportedFile { get; set; }

        /// <summary>
        /// Загруженный файл
        /// </summary>
        [Display(Name = "Вложенный файл")]
        public long? ImportedFileId { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        [Display(Name = "Дом")]
        public virtual House House { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        [Display(Name = "Дом")]
        public virtual long HouseId { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        [Display(Name = "Лицевой счет")]
        public virtual PersonalAccount PersonalAccount { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        [Display(Name = "Лицевой счет")]
        public virtual long PersonalAccountId { get; set; }

        /// <summary>
        /// Комментарии к заявке
        /// </summary>
        [Display(Name = "Комментарии к заявке")]
        public List<AbonentClaimCommentViewModel> CommentList { get; set; }

        /// <summary>
        /// Заявка принята
        /// </summary>
        [Display(Name = "Заявка принята")]
        public bool Accepted { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        [Display(Name = "Оценка")]
        public int? Rating { get; set; }

        /// <summary>
        /// Заявка закрыта сотрудником
        /// </summary>
        [Display(Name = "Заявка закрыта сотрудником")]
        public Employee ClosedByEmployee { get; set; }

        /// <summary>
        /// Заявка закрыта сотрудником
        /// </summary>
        [Display(Name = "Заявка закрыта сотрудником")]
        public long? ClosedByEmployeeId { get; set; }

        /// <summary>
        /// Стоимость заявки
        /// </summary>
        [Display(Name = "Стоимость")]
        public decimal? Cost { get; set; }
    }
}
