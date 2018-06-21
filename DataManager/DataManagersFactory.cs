namespace OhDataManager
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using AbstractDm;
    using Entities;

    /// <summary>
    /// Фабрика менеджеров данных
    /// </summary>
    public class DataManagersFactory : IDisposable
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DataManagersFactory()
        {
            _dmType = ConfigurationManager.AppSettings.Get("DataManagerType");
            if (string.IsNullOrEmpty(_dmType))
                throw new Exception("В конфигурационном файле не найдено значение для параметра 'DataManagerType'!");
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _personalAccountDataManager = null;
        }

        /// <summary>
        /// Тип менеджера данных
        /// </summary>
        private readonly string _dmType;

        /// <summary>
        /// Менеджер данных для лицевого счета
        /// </summary>
        private DmPersonalAccount _personalAccountDataManager;

        /// <summary>
        /// Получить менеджера данных для лицевого счета
        /// </summary>
        public DmPersonalAccount GetPersonalAccountDataManager()
        {
            return _personalAccountDataManager ??
                   (_personalAccountDataManager = (DmPersonalAccount) CreateDataManager("DmPersonalAccount"));
        }

        /// <summary>
        /// Создание менеджера данных 
        /// </summary>
        /// <param name="abstractDataManagerName"></param>
        /// <returns></returns>
        private IDataManager CreateDataManager(string abstractDataManagerName)
        {
            var instance =
                Assembly.GetExecutingAssembly()
                    .GetExportedTypes()
                    .SingleOrDefault(x => x.GetCustomAttributes(typeof (DataManagerAttribute), false)
                        .Select(y => (DataManagerAttribute) y)
                        .Any(z => z.DataManagerType == _dmType && z.AbstractDataManagerName == abstractDataManagerName));

            if (instance == null)
                throw new Exception(
                    "Не найдена соответствующая реализация менеджера данных для AbstractDataManagerName: " +
                    abstractDataManagerName);

            return (IDataManager) Activator.CreateInstance(instance);
        }
    }
}
