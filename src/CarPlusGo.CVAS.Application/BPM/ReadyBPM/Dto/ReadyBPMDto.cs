using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.BPM.Dto
{
    [AutoMap(typeof(ReadyBPM))]
    public class ReadyBPMDto:EntityDto<string>
    {
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
        public string TagId { get; set; }
    }
}
