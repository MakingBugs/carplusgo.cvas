using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile
{
    [Table("RRLKR")]
    public class RRLKR : FullAuditedEntity<long>
    {
        [Column("RRLKR_Auto")]
        public override long Id { get; set; }
        public int FormType { get; set; }
        [Column("LKR_User")]
        public string LkrUser { get; set; }
        [Column("LKR_Account")]
        public string LkrAccount { get; set; }
        [Column("LKR_Bank")]
        public string LkrBank { get; set; }
        public int BankType { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
    }
}
