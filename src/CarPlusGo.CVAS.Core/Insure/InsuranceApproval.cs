using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Insure.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Insure
{
    /// <summary>
    /// 保险签核
    /// </summary>
    [Table("InsuranceApproval")]
    public class InsuranceApproval : FullAuditedEntity<long>
    {
        /// <summary>
        /// 签核类别
        /// </summary>
        public InsuranceApprovalType InsuranceApprovalType { get; set; }
        /// <summary>
        /// 签核状态
        /// </summary>
        public InsuranceApprovalStatus InsuranceApprovalStatus { get; set; }
        /// <summary>
        /// 送签金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 额外金额
        /// </summary>
        public decimal ExtraAmount { get; set; }
        /// <summary>
        /// 送签人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 保险发票类别
        /// </summary>
        public InsuranceInvoiceType InsuranceInvoiceType { get; set; }
        /// <summary>
        /// 发票号码
        /// </summary>
        public string InvoiceNumber { get; set; }
        /// <summary>
        /// 发票代码
        /// </summary>
        public string InvoiceCode { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string InvoiceName { get; set; }
        /// <summary>
        /// 纳税人识别号
        /// </summary>
        public string TaxpayerIdentificationNumber { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 开户银行
        /// </summary>
        public string Bank { get; set; }
        /// <summary>
        /// 银行账户
        /// </summary>
        public string BankAccount { get; set; }
    }
}
