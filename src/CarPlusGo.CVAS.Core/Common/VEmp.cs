using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    [Table("v_emp")]
    public class VEmp : Entity<long>
    {
        [Column("EmpBase_Auto")]
        public override long Id { get; set; }
        public string UPOrg4Name { get; set; }
        public long UPOrg5 { get; set; }
        public string UPOrg3Name { get; set; }
        public long UPOrg4 { get; set; }
        public string UPOrg2Name { get; set; }
        public long UPOrg3 { get; set; }
        public string UPOrgName { get; set; }
        public long UPOrg2 { get; set; }
        [Column("TradeItem_Auto")]
        public long TradeItemAuto { get; set; }
        public Guid UserId { get; set; }
        [Column("User_Auto")]
        public long UserAuto { get; set; }
        [Column("Org_Auto")]
        public long? OrgAuto { get; set; }
        public string DepCode { get; set; }
        public int? Lev { get; set; }
        public long? UpUnit { get; set; }
        public int? TitleLevel { get; set; }
        public string DepName { get; set; }
        public string TitleName { get; set; }
        public string FName { get; set; }	
        public string UserName { get; set; }
        public int IsOn { get; set; }
        public int? IsEas { get; set; }
        public string AccCode { get; set; }
        public int? IsSalesDep { get; set; }
        public string BPMUserID { get; set; }
        public string BPMUserName { get; set; }
        public string BPMDeptID { get; set; }
        public string BPMDeptName { get; set; }
    }
}
