using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Платежные реквизиты
    /// </summary>
    [Table(nameof(PersonalAccountPaymentRequisite), Schema = "main")]
    public class PersonalAccountPaymentRequisite : BaseOhEntity, IPersonalAccount, IBillInfo
    {
        /// <summary>
        /// Дата расчета (расчетный месяц)
        /// </summary>
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        public virtual PersonalAccount PersonalAccount { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        public virtual long PersonalAccountId { get; set; }
        
        /// <summary>
        /// Номер лицевого счета
        /// </summary>
        public long PersonalAccountNumber { get; set; }

        /// <summary>
        /// Платежный код
        /// </summary>
        public long PaymentCode { get; set; }

        /// <summary>
        /// Полное наименование адреса (до комнаты)
        /// </summary>
        public string ApartmentFullAddress { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        public House House { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        public long HouseId { get; set; }

        /// <summary>
        /// Тип реквизитов
        /// </summary>
        public int RequisiteType { get; set; }

        /// <summary>
        /// Имя получателя средств
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// Наименование банка получателя
        /// </summary>
        public string RecipientBankName { get; set; }

        /// <summary>
        /// Расчетный счет банка получателя
        /// </summary>
        public string RecipientCheckingAccount { get; set; }

        /// <summary>
        /// Корр.счет банка получателя
        /// </summary>
        public string RecipientCorrespondentAccount { get; set; }

        /// <summary>
        /// ИНН получателя
        /// </summary>
        public string RecipientInn { get; set; }

        /// <summary>
        /// КПП получателя
        /// </summary>
        public string RecipientKpp { get; set; }

        /// <summary>
        /// БИК банка получателя
        /// </summary>
        public string RecipientBankBik { get; set; }

        /// <summary>
        /// Адрес получателя
        /// </summary>
        public string RecipientAddress { get; set; }

        /// <summary>
        /// Телефон получателя
        /// </summary>
        public string RecipientPhone { get; set; }

        /// <summary>
        /// Адрес электронной почты получателя
        /// </summary>
        public string RecipientEmail { get; set; }

        /// <summary>
        /// Примечание от получателя
        /// </summary>
        public string RecipientNote { get; set; }

        /// <summary>
        /// Имя исполнителя средств
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Наименование банка исполнителя
        /// </summary>
        public string ProviderBankName { get; set; }

        /// <summary>
        /// Расчетный счет банка исполнителя
        /// </summary>
        public string ProviderCheckingAccount { get; set; }

        /// <summary>
        /// Корр.счет банка исполнителя
        /// </summary>
        public string ProviderCorrespondentAccount { get; set; }

        /// <summary>
        /// ИНН исполнителя
        /// </summary>
        public string ProviderInn { get; set; }

        /// <summary>
        /// КПП исполнителя
        /// </summary>
        public string ProviderKpp { get; set; }

        /// <summary>
        /// БИК банка исполнителя
        /// </summary>
        public string ProviderBankBik { get; set; }

        /// <summary>
        /// Адрес исполнителя
        /// </summary>
        public string ProviderAddress { get; set; }

        /// <summary>
        /// Телефон исполнителя
        /// </summary>
        public string ProviderPhone { get; set; }

        /// <summary>
        /// Адрес электронной почты исполнителя
        /// </summary>
        public string ProviderEmail { get; set; }

        /// <summary>
        /// Примечание от исполнителя
        /// </summary>
        public string ProviderNote { get; set; }

        /// <summary>
        /// Код УК
        /// </summary>
        public string ManagmentOrganizationCode { get; set; }

        /// <summary>
        /// Свободный текст
        /// </summary>
        public string Note { get; set; }
    }
}
