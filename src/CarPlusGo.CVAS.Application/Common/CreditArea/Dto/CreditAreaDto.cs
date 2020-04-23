using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(CreditArea))]
    public class CreditAreaDto : EntityDto<long>
    {
        public long Code { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
    }
}
