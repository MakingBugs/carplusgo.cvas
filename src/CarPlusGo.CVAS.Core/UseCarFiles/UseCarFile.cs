using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.UseCarFiles
{
    [Table("UseCarFile")]
    public class UseCarFile : FullAuditedEntity<long>
    {
        [Column("UseCarFileID")]
        public override long Id { get; set; }
        public long RepositoryOutID { get; set; }//出入库档ID
        public int Type { get; set; }//出入库记录 1.出庫 2.入庫
        public string FileName { get; set; }//文件名称
        public int Status { get; set; }//状态
        public string Path { get; set; }//url
    }
}
