using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.UseCarFiles
{
    [Table("UseCarPart")]
    public class UseCarPart : FullAuditedEntity<long>
    {
        [Column("UseCarPartID")]
        public override long Id { get; set; }
        public long RepositoryOutID { get; set; }//出入库档ID
        public int Type { get; set; }//出入库记录 1出库 2入库
        public long CarPartID { get; set; }//车辆部位
        public int Status { get; set; }//车辆提领状态
        public string Memo { get; set; }//备注
    }
}
