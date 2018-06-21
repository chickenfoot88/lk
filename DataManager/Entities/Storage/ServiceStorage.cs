using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// ������
    /// </summary>
    [Table("Service", Schema = "storage")]
    public class ServiceStorage : Base.BaseOhEntity, IService
    {
        /// <summary>
        /// ������������ ������
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// ������������� ������ �� ������� �������
        /// </summary>
        public long OuterSystemServiceId { get; set; }

        /// <summary>
        /// ������������� ������� ������ �� ������� �������
        /// </summary>
        public long? OuterSystemBaseServiceId { get; set; }

        /// <summary>
        /// ������� ���������
        /// </summary>
        public string MeasureName { get; set; }

        /// <summary>
        /// ������� ������
        /// </summary>
        public ServiceStorage BaseService { get; set; }
        
        /// <summary>
        /// ������� ������
        /// </summary>
        public long? BaseServiceId { get; set; }

        /// <summary>
        /// ������ ����� (��������, ������������ � �.�.)
        /// </summary>
        public string ServiceGroupName { get; set; }
    }
}