using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(BankType))]
    public class BankTypeDto : EntityDto<int>
    {
        public string BankNameT { get; set; }
        public int Invisible { get; set; }
    }
}
