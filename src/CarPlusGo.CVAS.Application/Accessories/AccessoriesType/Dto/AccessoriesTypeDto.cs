using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.Accessories.Dto
{
    [AutoMap(typeof(AccessoriesType))]
    public class AccessoriesTypeDto : EntityDto<long>
    {
        public long AccessoriesMainTypeAuto { get; set; }
        public AccessoriesMainTypeDto AccessoriesMainType { get; set; }
        public string AccessoriesTypeName { get; set; }
        public int? PurchasePrice { get; set; }
        public int? SellingPrice { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
    }
}
