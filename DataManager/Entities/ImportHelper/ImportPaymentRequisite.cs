using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OhDataManager.Entities.ImportHelper
{
    /// <summary>
    /// ������
    /// </summary>
    [Table("importpaymentrequisite", Schema = "import")]
    public class ImportPaymentRequisite : IBaseImportHelper
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
        /// ��� ����������
        /// </summary>
        [Key, Column(Order = 4)]
        public int RequisiteType { get; set; }

        /// <summary>
        /// ��� ���������� �������
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// ������������ ����� ����������
        /// </summary>
        public string RecipientBankName { get; set; }

        /// <summary>
        /// ��������� ���� ����� ����������
        /// </summary>
        public string RecipientCheckingAccount { get; set; }

        /// <summary>
        /// ����.���� ����� ����������
        /// </summary>
        public string RecipientCorrespondentAccount { get; set; }

        /// <summary>
        /// ��� ����������
        /// </summary>
        public string RecipientInn { get; set; }

        /// <summary>
        /// ��� ����������
        /// </summary>
        public string RecipientKpp { get; set; }

        /// <summary>
        /// ��� ����� ����������
        /// </summary>
        public string RecipientBankBik { get; set; }

        /// <summary>
        /// ����� ����������
        /// </summary>
        public string RecipientAddress { get; set; }

        /// <summary>
        /// ������� ����������
        /// </summary>
        public string RecipientPhone { get; set; }

        /// <summary>
        /// ����� ����������� ����� ����������
        /// </summary>
        public string RecipientEmail { get; set; }

        /// <summary>
        /// ���������� �� ����������
        /// </summary>
        public string RecipientNote { get; set; }
        
        /// <summary>
        /// ��� ����������� �������
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// ������������ ����� �����������
        /// </summary>
        public string ProviderBankName { get; set; }

        /// <summary>
        /// ��������� ���� ����� �����������
        /// </summary>
        public string ProviderCheckingAccount { get; set; }

        /// <summary>
        /// ����.���� ����� �����������
        /// </summary>
        public string ProviderCorrespondentAccount { get; set; }

        /// <summary>
        /// ��� �����������
        /// </summary>
        public string ProviderInn { get; set; }

        /// <summary>
        /// ��� �����������
        /// </summary>
        public string ProviderKpp { get; set; }

        /// <summary>
        /// ��� ����� �����������
        /// </summary>
        public string ProviderBankBik { get; set; }

        /// <summary>
        /// ����� �����������
        /// </summary>
        public string ProviderAddress { get; set; }

        /// <summary>
        /// ������� �����������
        /// </summary>
        public string ProviderPhone { get; set; }

        /// <summary>
        /// ����� ����������� ����� �����������
        /// </summary>
        public string ProviderEmail { get; set; }

        /// <summary>
        /// ���������� �� �����������
        /// </summary>
        public string ProviderNote { get; set; }
        
        /// <summary>
        /// ��� ��
        /// </summary>
        public string ManagmentOrganizationCode { get; set; }
        
        /// <summary>
        /// ��������� �����
        /// </summary>
        public string Note { get; set; }
    }
}