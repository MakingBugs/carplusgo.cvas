using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.BPM
{
    [Table("v_Emp")]
    public class UserInfo : Entity<long>
    {
        [Column("User_Auto")]
        public override long Id { get; set; }
        public string BPMUserID { get; set; }
        public string BPMUserName { get; set; }
        public string BPMDeptID { get; set; }
        public string BPMDeptName { get; set; }
    }
}
