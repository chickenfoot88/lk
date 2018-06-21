using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// ��������� �����
    /// </summary>
    [Table("Supplier", Schema = "storage")]
    public class SupplierStorage : Base.BaseOhEntity, ISupplier
    {
        /// <summary>
        /// ������������ ����������
        /// </summary>
        public string SupplierName { get; set; }
    }
}