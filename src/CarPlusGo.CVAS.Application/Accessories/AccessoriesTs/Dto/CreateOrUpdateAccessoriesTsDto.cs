using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Timing;
using System;

namespace CarPlusGo.CVAS.Accessories.Dto
{
    [AutoMap(typeof(AccessoriesTs))]
    public class CreateOrUpdateAccessoriesTsDto : EntityDto<long>
    {
        public long AccessoriesTypeAuto { get; set; }
        public int ItemType { get; set; }
        public long Supplier { get; set; }
        public int PurchasePrice { get; set; }
        public int SellingPrice { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
        public int? CostPrice { get; set; }

        public CreateOrUpdateAccessoriesTsDto()
        {
            Cdt = Clock.Now;
            ItemType = 883;
        }
    }
}
