using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace CarPlusGo.CVAS.LocationFile
{
    [Table("Location")]
    public class Location:FullAuditedEntity<long>
    {
        [Column("AreaID")]
        public override long Id { get; set; }
        public string AreaName { get; set; }
        public int IsStop { get; set; }
    }
}
