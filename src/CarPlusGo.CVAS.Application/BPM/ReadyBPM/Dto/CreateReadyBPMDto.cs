using Abp.AutoMapper;

namespace CarPlusGo.CVAS.BPM.Dto
{
    [AutoMap(typeof(ReadyBPM))]
    public class CreateReadyBPMDto
    {
        public string FormName { get; set; }
        public string Id { get; set; }
        public string FlowId { get; set; }
    }
}
