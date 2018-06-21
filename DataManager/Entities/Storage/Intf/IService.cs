using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Storage.Intf
{
    /// <summary>
    /// Услуга
    /// </summary>
    public interface IService : IBaseEntity
    {
        /// <summary>
        /// Наименование услуги
        /// </summary>
        string ServiceName { get; set; }

        /// <summary>
        /// Идентификатор услуги во внешней системе
        /// </summary>
        long OuterSystemServiceId { get; set; }

        /// <summary>
        /// Идентификатор базовой услуги во внешней системе
        /// </summary>
        long? OuterSystemBaseServiceId { get; set; }

        /// <summary>
        /// Базовая услуга
        /// </summary>
        ServiceStorage BaseService { get; set; }

        /// <summary>
        /// Базовая услуга
        /// </summary>
        long? BaseServiceId { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        string MeasureName { get; set; }
        
        /// <summary>
        /// Группа услуг (жилищные, коммунальные и т.д.)
        /// </summary>
        string ServiceGroupName { get; set; }
    }
}