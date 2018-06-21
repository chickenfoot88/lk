using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// ��������� �����
    /// </summary>
    [Table("Supplier", Schema = "main")]
    public class Supplier : Base.BaseOhEntity, ISupplier
    {
        /// <summary>
        /// ������������ ����������
        /// </summary>
        public string SupplierName { get; set; }
    }
}