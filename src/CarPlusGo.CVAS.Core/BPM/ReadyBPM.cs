using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.BPM
{
    [Table("ReadyBPM")]
    public class ReadyBPM:Entity<string>
    {
        [Column("RequisitionId")]
        public override string Id { get; set; }
        public string DiagramId { get; set; }
        public string ApplicantDept { get; set; }
        public string ApplicantDeptName { get; set; }
        public string ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public string FillerId { get; set; }
        public string FillerName { get; set; }
        public DateTime? ApplicantDateTime { get; set; }
        public int? Priority { get; set; }
        public int? DraftFlag { get; set; }
        public int? FlowActivated { get; set; }
        [Column("Id")]
        public string TagId { get; set; }
    }
}
