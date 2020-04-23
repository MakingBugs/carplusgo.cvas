using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.Car.Dto
{
    [AutoMap(typeof(Brand))]
    public class BrandDto : EntityDto<long>
    {
        public string BrandName { get; set; }
        public DateTime Cdt { get; set; }
        public long Cuser { get; set; }
        public DateTime Mdt { get; set; }
        public long Muser { get; set; }
        public FactoryBrandDto FactoryBrand { get; set; }
    }
}
