using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    /// <summary>
    /// 厂商联系人
    /// </summary>
    [Table("SupplierContect")]
    public class SupplierContect : FullAuditedEntity<long>
    {
        [Column("SupplierContect_Auto")]
        public override long Id { get; set; } 
        [Column("Supplier_Auto")]
        public long SupplierAuto { get; set; }
        public string  Title { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public string MTel { get; set; }
        public string Fax { get; set; }
        [ForeignKey("CreditProvince")]
        public long? Province { get; set; }
        public CreditProvince CreditProvince { get; set; }
        [ForeignKey("CreditCity")]
        public long? City { get; set; }
        public CreditCity CreditCity { get; set; }
        [ForeignKey("CreditArea")]
        public long? Area { get; set; }
        public CreditArea CreditArea { get; set; }
        public string Address { get; set; }
    }
}
