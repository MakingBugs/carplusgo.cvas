using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Common.Dto;
using System;

namespace CarPlusGo.CVAS.Accessories.Dto
{
    [AutoMap(typeof(AccessoriesTs))]
    public class AccessoriesTsDto : EntityDto<long>
    {
        public long AccessoriesTypeAuto { get; set; }
        public long Supplier { get; set; }
        public int ItemType { get; set; }
        public AccessoriesTypeDto AccessoriesType { get; set; }
        public ItemCodeDto ItemCode { get; set; }
        public int PurchasePrice { get; set; }
        public int SellingPrice { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
        public int? CostPrice { get; set; }
    }
}
