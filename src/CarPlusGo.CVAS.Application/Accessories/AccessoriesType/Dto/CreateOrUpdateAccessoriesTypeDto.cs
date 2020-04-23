using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Timing;
using System;

namespace CarPlusGo.CVAS.Accessories.Dto
{
    [AutoMap(typeof(AccessoriesType))]
    public class CreateOrUpdateAccessoriesTypeDto : EntityDto<long>
    {
        public long AccessoriesMainTypeAuto { get; set; }
        public string AccessoriesTypeName { get; set; }
        public int? PurchasePrice { get; set; }
        public int? SellingPrice { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
        public CreateOrUpdateAccessoriesTypeDto()
        {
            Cdt = Clock.Now;
        }
    }
}
