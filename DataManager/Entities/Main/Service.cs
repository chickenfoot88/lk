using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// ������
    /// </summary>
    [Table("Service", Schema = "main")]
    public class Service : BaseOhEntity, IService
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
        public Service BaseService { get; set; }

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