using System;
using System.Configuration;
using OhUtils;

namespace WebAppAuth
{
    /// <summary>
    /// Класс констант
    /// </summary>
    public static class AppConstants
    {
        /// <summary>
        /// Роль "Администратор"
        /// </summary>
        public const string RoleAdministrator = "Administrator";

        /// <summary>
        /// Роль "Абонент"
        /// </summary>
        public const string RoleAbonent = "Abonent";

        /// <summary>
        /// Роль "Руководитель"
        /// </summary>
        public const string RoleDirector = "Director";

        /// <summary>
        /// Роль "Диспетчер"
        /// </summary>
        public const string RoleEmployee = "Employee";
        
        /// <summary>
        /// Роль "Рабочий"
        /// </summary>
        public const string RoleWorker = "Worker";

        /// <summary>
        /// Ссылка на публичную часть портала
        /// </summary>
        public static string PublicSiteUrl
        {
            get
            {
                var publicSiteUrl = ConfigurationManager.AppSettings.Get("PublicSiteUrl");
                if (string.IsNullOrEmpty(publicSiteUrl))
                    throw new Exception("В конфигурационном файле не найдено значение для параметра 'PublicSiteUrl'!");

                return publicSiteUrl;
            }
        }

        /// <summary>
        /// Наименование стиля UI
        /// </summary>
        public static string UiStyleName
        {
            get
            {
                var uiStyleName = ConfigurationManager.AppSettings.Get("UiStyleName");
                if (string.IsNullOrEmpty(uiStyleName))
                    uiStyleName = "default";

                return uiStyleName;
            }
        }

        /// <summary>
        /// Доступен ли режим оплаты 
        /// (true - доступен, false - не доступен)
        /// </summary>
        public static bool PaymentAllowed
        {
            get
            {
                try
                {
                    var paymentAllowed = ConfigurationManager.AppSettings.Get("PaymentAllowed");
                    if (string.IsNullOrEmpty(paymentAllowed))
                        paymentAllowed = "false";

                    var result = Convert.ToBoolean(paymentAllowed);

                    return result;
                }
                catch (Exception exception)
                {
                    LogUtils.WriteLog(exception.Message, LogUtils.ELogType.Info);
                    return false;
                }
            }
        }

        /// <summary>
        /// Логин к ЛК провайдера платежей
        /// </summary>
        public static string PaymentProviderLogin
        {
            get
            {
                var paymentProviderLogin = ConfigurationManager.AppSettings.Get("PaymentProviderLogin");
                if (string.IsNullOrEmpty(paymentProviderLogin))
                    throw new Exception("В конфигурационном файле не найдено значение для параметра 'PaymentProviderLogin'!");

                return paymentProviderLogin;
            }
        }


        /// <summary>
        /// Пароль к ЛК провайдера платежей
        /// </summary>
        public static string PaymentProviderPassword
        {
            get
            {
                var paymentProviderPassword = ConfigurationManager.AppSettings.Get("PaymentProviderPassword");
                if (string.IsNullOrEmpty(paymentProviderPassword))
                    throw new Exception("В конфигурационном файле не найдено значение для параметра 'PaymentProviderPassword'!");

                return paymentProviderPassword;
            }
        }

        /// <summary>
        /// Идентификатор точки продаж
        /// </summary>
        public static string SalePointId
        {
            get
            {
                var salePointId = ConfigurationManager.AppSettings.Get("SalePointId");
                if (string.IsNullOrEmpty(salePointId))
                    throw new Exception("В конфигурационном файле не найдено значение для параметра 'SalePointId'!");

                return salePointId;
            }
        }

        /// <summary>
        /// Доступен ли модуль заявок 
        /// (true - доступен, false - не доступен)
        /// </summary>
        public static bool ClaimsModuleAllowed
        {
            get
            {
                try
                {
                    var сlaimsModuleAllowed = ConfigurationManager.AppSettings.Get("ClaimsModuleAllowed");
                    if (string.IsNullOrEmpty(сlaimsModuleAllowed))
                        сlaimsModuleAllowed = "false";

                    var result = Convert.ToBoolean(сlaimsModuleAllowed);

                    return result;
                }
                catch (Exception exception)
                {
                    LogUtils.WriteLog(exception.Message, LogUtils.ELogType.Info);
                    return false;
                }
            }
        }
    }
}
