using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.Common
{
    [Table("PRInvLink")]
    public class PRInvLink : FullAuditedEntity<long>
    {
        [Column("PRInvLink_Auto")]
        public override long Id { get; set; }
        [Column("PRInv_Auto")]
        public long PrinvAuto { get; set; }
        public int Prtype { get; set; }
        public int InvSource { get; set; }
        [Column("Source_Auto")]
        public long SourceAuto { get; set; }
        public decimal LinkAmt { get; set; }
    }
}
