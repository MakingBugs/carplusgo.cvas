using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(CreditProvince))]
    public class CreditProvinceDto : EntityDto<long>
    {
        public long Code { get; set; }
        public string Name { get; set; }
    }
}
