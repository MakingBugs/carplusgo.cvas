using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.LocationFile
{
    [Table("Repository")]
    public class Repository : FullAuditedEntity<long>
    {
        [Column("RepositoryID")]
        public override long Id { get; set; }
        public int RepositoryType { get; set; }
        public string RepositoryName { get; set; }
        public long AreaID { get; set; }
        public int IsStop { get; set; }
        [ForeignKey("AreaID")]
        public Location Location { get; set; }
    }
}
