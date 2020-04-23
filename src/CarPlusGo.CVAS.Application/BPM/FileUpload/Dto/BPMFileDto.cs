using Abp.AutoMapper;

namespace CarPlusGo.CVAS.BPM.Dto
{
    [AutoMap(typeof(BPMFile))]
    public class BPMFileDto
    {
        public string RequisitionID { get; set; }
        public string FormName { get; set; }
        public string NFileName { get; set; }
        public string OFileName { get; set; }
        public int FileSize { get; set; }
        public string FlowId { get; set; }
    }
}
