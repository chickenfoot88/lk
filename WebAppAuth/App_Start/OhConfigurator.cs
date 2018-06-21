using System.Globalization;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using OhDataManager.Enums;
using WebAppAuth.Services.Data;
using WebAppAuth.Services.Export;
using WebAppAuth.Services.FileManager;
using WebAppAuth.Services.Import;

namespace WebAppAuth
{
    /// <summary>
    /// Установщик
    /// </summary>
    public class OhConfigurator
    {
        private static IWindsorContainer _container;

        /// <summary>
        /// Контейнер
        /// </summary>
        public static IWindsorContainer Container => _container ?? (_container = new WindsorContainer());

        /// <summary>
        /// Установка необходимых компонентов
        /// </summary>
        public static void Inclusion()
        {
            SetCulture();
            RegisterServices();
        }

        /// <summary>
        /// Установка региональных настроек
        /// </summary>
        private static void SetCulture()
        {
            var culture = new CultureInfo("ru-RU")
            {
                NumberFormat =
                {
                    NumberDecimalSeparator = ".",
                    CurrencyDecimalSeparator = "."
                },
                DateTimeFormat =
                {
                    ShortDatePattern = "dd.MM.yyyy",
                    ShortTimePattern = "HH:mm:ss"
                }
            };

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        /// <summary>
        /// Регистрация сервисов
        /// </summary>
        private static void RegisterServices()
        {
            Container.Register(Component.For<IFileStorageManager>()
                .ImplementedBy<FileStorageManager>()
                .LifestyleSingleton());

            Container.Register(
                Component.For<BaseFileImport>()
                    .ImplementedBy<AccountImportService>()
                    .LifestyleTransient()
                    .Named(EImportType.Account.GetDisplayName()));
            
            Container.Register(
                Component.For<BaseFileExport>()
                    .ImplementedBy<OhMeterReadingExportService>()
                    .LifestyleTransient()
                    .Named(EExportType.MeterReading.GetDisplayName()));

            Container.Register(
                Component.For<IAbonentService>()
                    .ImplementedBy<AbonentService>()
                    .LifestyleTransient());

            Container.Register(
                Component.For<ISystemInfoService>()
                    .ImplementedBy<SystemInfoService>()
                    .LifestyleTransient());
        }
    }
}
