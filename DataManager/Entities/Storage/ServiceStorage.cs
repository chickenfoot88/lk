using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// Услуга
    /// </summary>
    [Table("Service", Schema = "storage")]
    public class ServiceStorage : Base.BaseOhEntity, IService
    {
        /// <summary>
        /// Наименование услуги
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Идентификатор услуги во внешней системе
        /// </summary>
        public long OuterSystemServiceId { get; set; }

        /// <summary>
        /// Идентификатор базовой услуги во внешней системе
        /// </summary>
        public long? OuterSystemBaseServiceId { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string MeasureName { get; set; }

        /// <summary>
        /// Базовая услуга
        /// </summary>
        public ServiceStorage BaseService { get; set; }
        
        /// <summary>
        /// Базовая услуга
        /// </summary>
        public long? BaseServiceId { get; set; }

        /// <summary>
        /// Группа услуг (жилищные, коммунальные и т.д.)
        /// </summary>
        public string ServiceGroupName { get; set; }
    }
}