using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Car.Dto;
using CarPlusGo.CVAS.Common.Dto;
using CarPlusGo.CVAS.LocationFile.Dto;

namespace CarPlusGo.CVAS.TakeCarFile.Dto
{
    [AutoMap(typeof(TakeCarApply))]//车辆提领申请
    public class TakeCarApplyDto : FullAuditedEntityDto<long>
    {
        public long? Type { get; set; }//领用类型
        public long AreaID { get; set; }//提领区域
        public DateTime TakeDate { get; set; }//提领日期
        public int TakeQty { get; set; }//提领数量
        public long? Factory { get; set; }//总厂牌
        public long? Brand { get; set; }//厂牌
        public long? Clasen { get; set; }//车辆
        public string Memo { get; set; }//备注
        public string CancelMemo { get; set; }//取消备注
        public long? Status { get; set; }//提领申请状态
        public int? ItemType { get; set; }
        public int? ItemStatus { get; set; }
        public LocationDto Location { get; set; }
        public FactoryBrandDto FactoryBrand { get; set; }
        public BrandDto BrandData { get; set; }
        public ClasenDto ClasenData { get; set; }
        public ItemCodeDto ItemTypeCode { get; set; }
        public ItemCodeDto ItemStatusCode { get; set; }
    }
}
