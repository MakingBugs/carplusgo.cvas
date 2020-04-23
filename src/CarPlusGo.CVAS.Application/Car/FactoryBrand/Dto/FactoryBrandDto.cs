using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.Car.Dto
{
    [AutoMap(typeof(FactoryBrand))]
    public class FactoryBrandDto : EntityDto<long>
    {
        public string FactoryBrandName { get; set; }
        public DateTime Cdt { get; set; }
        public long Cuser { get; set; }
        public DateTime Mdt { get; set; }
        public long Muser { get; set; }
    }
}
