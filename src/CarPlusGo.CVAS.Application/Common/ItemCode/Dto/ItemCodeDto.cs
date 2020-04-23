using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(ItemCode))]
    public class ItemCodeDto : EntityDto<long>
    {
        public int ItemType { get; set; }
        public long Num { get; set; }
        public string ItemName { get; set; }
        public string Memo { get; set; }
        public int IsActive { get; set; }
        public int Seq { get; set; }
        public decimal V1 { get; set; }
        public decimal V2 { get; set; }
        public decimal A1 { get; set; }
    }
}
