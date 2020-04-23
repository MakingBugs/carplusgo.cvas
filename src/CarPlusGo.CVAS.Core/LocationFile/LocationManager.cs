using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.LocationFile
{
    [Table("LocationManager")]
    public class LocationManager:FullAuditedEntity<long>
    {
        [Column("AreaManagerID")]
        public override long Id { get; set; }
        public long AreaID { get; set; }
        public int RepositoryType { get; set; }
        public string EmpID { get; set; }
        public int IsStop { get; set; }
        [ForeignKey("AreaID")]
        public Location Location { get; set; }
    }
}
