using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Common;

namespace CarPlusGo.CVAS.Car
{
    [Table("CarCertificateRight")]
    public class CarCertificateRight:FullAuditedEntity<long>
    {
        [Column("CarCertificateRightID")]
        public override long Id { get; set; }
        public long CarCertificateID { get; set; }
        public long? OilID { get; set; }
        public int Selected { get; set; }
        public int? OilType { get; set; }
        [ForeignKey("OilType,OilID")]
        public ItemCode ItemCode { get; set; }
        [ForeignKey("CarCertificateID")]
        public CarCertificate CarCertificate { get; set; }

    }
}
