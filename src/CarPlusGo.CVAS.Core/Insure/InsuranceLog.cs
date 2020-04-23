using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Insure.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Insure
{
    /// <summary>
    /// 保险变更记录
    /// </summary>
    [Table("InsuranceLog")]
    public class InsuranceLog: CreationAuditedEntity<long>
    {
        public InsuranceOperationType InsuranceOperationType { get; set; }
        public long InsuranceDetailId { get; set; }
        public string SerialNumber { get; set; }
        public string OldInsuranceJson { get; set; }
        public string NewInsuranceJson { get; set; }
    }
}
