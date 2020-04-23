using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.BPM
{
    [Table("FormFlow")]
    public class FormFlow:Entity<int>
    {
        [Column("FormFlow_Auto")]
        public override int Id { get; set; }
        public string FormName { get; set; }
        public string FlowId { get; set; }
        public string BpmFormName { get; set; }
        public string BpmFormId { get; set; }
        public string FlowType { get; set; }
    }
}
