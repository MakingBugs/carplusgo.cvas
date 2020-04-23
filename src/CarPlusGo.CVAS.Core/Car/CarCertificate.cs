using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace CarPlusGo.CVAS.Car
{
    [Table("CarCertificate")]
    public class CarCertificate:FullAuditedEntity<long>
    {
        [Column("CarCertificateID")]
        public override long Id { get; set; }
        public string CarCertificateName { get; set; }
        public int Qty { get; set; }
        public int IsStop { get; set; }
    }
}
