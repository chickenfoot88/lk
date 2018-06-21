using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Услуга
    /// </summary>
    [Table("Service", Schema = "main")]
    public class Service : BaseOhEntity, IService
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
        public Service BaseService { get; set; }

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