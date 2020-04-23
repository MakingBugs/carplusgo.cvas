using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(CreditCity))]
    public class CreditCityDto : EntityDto<long>
    {
        public long Code { get; set; }
        public string Name { get; set; }
        public long ProvinceId { get; set; }
    }
}
