using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Main;
using WebAppAuth.Models;

namespace WebAppAuth.Entities
{
    /// <summary>
    /// Абонент
    /// </summary>
    [Table(nameof(Abonent), Schema = "main")]
    public class Abonent : BaseOhEntity
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Платежный код
        /// </summary>
        public virtual long PaymentCode { get; set; }

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
        public virtual string ApartmentFullAddress { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [Index("IX_Abonent_ApplicationUser", 1, IsUnique = true)]
        public virtual int ApplicationUserId { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public virtual string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public virtual string Patronymic { get; set; }

        /// <summary>
        /// Телефон домашний
        /// </summary>
        public virtual string HousePhoneNumber { get; set; }

        /// <summary>
        /// Телефон сотовый
        /// </summary>
        public virtual string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Разрешить SMS информирование
        /// </summary>
        public virtual bool SmsNotificationAllowed { get; set; }

        /// <summary>
        /// Разрешить e-mail информирование
        /// </summary>
        public virtual bool EmailNotificationAllowed { get; set; }

        /// <summary>
        /// Аватар
        /// </summary>
        public virtual byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }
    }
}