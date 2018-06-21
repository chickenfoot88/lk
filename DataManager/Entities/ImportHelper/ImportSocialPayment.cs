using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OhDataManager.Entities.ImportHelper
{
    /// <summary>
    /// ���������� �������
    /// </summary>
    [Table("importsocialpayment", Schema = "import")]
    public class ImportSocialPayment : IBaseImportHelper
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
        /// ��� ���������� �������
        /// </summary>
        [Key, Column(Order = 4)]
        public string RecipientName { get; set; }

        /// <summary>
        /// ��� ������ ��������������
        /// </summary>
        [Key, Column(Order = 5)]
        public long? ArticleCode { get; set; }

        /// <summary>
        /// ������ ��������������
        /// </summary>
        [Column(Order = 6)]
        public string ArticleName { get; set; }

        /// <summary>
        /// ������ ������
        /// </summary>
        [Column(Order = 7)]
        public string ArticleGroupName { get; set; }

        /// <summary>
        /// ����������� �����
        /// </summary>
        [Key, Column(Order = 8)]
        public decimal? SumAccrued { get; set; }

        /// <summary>
        /// ����� � �������
        /// </summary>
        public decimal? SumPayoff { get; set; }

        /// <summary>
        /// ����� �������
        /// </summary>
        public string PaymentPlacement { get; set; }

        /// <summary>
        /// ���� ��������� �������������� ��������
        /// </summary>
        public DateTime? SubsidiesEndDate { get; set; }

        /// <summary>
        /// �������� ������(����+/���������-)
        /// </summary>
        public decimal? SumInsaldo { get; set; }

        /// <summary>
        /// ���������
        /// </summary>
        public decimal? SumDelta { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        public decimal? SumRecalculation { get; set; }
    }
}