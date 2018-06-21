using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OhDataManager.Entities.ImportHelper
{
    /// <summary>
    /// ������
    /// </summary>
    [Table("importpayment", Schema = "import")]
    public class ImportPayment : IBaseImportHelper
    {
        /// <summary>
        /// ������������� ������
        /// </summary>
        [Column(Order = 0)]
        public long SectionId { get; set; }

        /// <summary>
        /// ��������� �����
        /// </summary>
        [Key, Column(Order = 1)]
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// ��� �����������
        /// </summary>
        [Key, Column(Order = 2)]
        public long OrganizationCode { get; set; }

        /// <summary>
        /// ��������� ���
        /// </summary>
        [Key, Column(Order = 3)]
        public long PaymentCode { get; set; }

        /// <summary>
        /// ���� �������
        /// </summary>
        [Key, Column(Order = 4)]
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// ���� ����� ��������� ������� � �������
        /// </summary>
        [Key, Column(Order = 5)]
        public DateTime CalculationApplyingDate { get; set; }

        /// <summary>
        /// �� ����� ����� ��������
        /// </summary>
        [Key, Column(Order = 6)]
        public DateTime PaydCalculationMonth { get; set; }

        /// <summary>
        /// ����� �������
        /// </summary>
        [Key, Column(Order = 7)]
        public decimal SumPayment { get; set; }

        /// <summary>
        /// ����� ���������� �������
        /// </summary>
        public string PaymentPlacement { get; set; }

    }
}