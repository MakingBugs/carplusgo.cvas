using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.LocationFile
{
    [Table("RepositoryManager")]
    public class RepositoryManager : FullAuditedEntity<long>
    {
        [Column("RepositoryManagerID")]
        public override long Id { get; set; }
        public long RepositoryID { get; set; }//仓库ID
        public int ManagerID { get; set; }//员工编号
        public int IsStop { get; set; }//是否停用
        public int IsManager { get; set; }//是否是库长
        [ForeignKey("RepositoryID")]
        public Repository Repository { get; set; }
    }
}
