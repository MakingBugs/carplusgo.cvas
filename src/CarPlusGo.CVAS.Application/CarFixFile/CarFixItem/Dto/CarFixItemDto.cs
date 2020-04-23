using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile.Dto
{
    [AutoMap(typeof(CarFixItem))]
    public class CarFixItemDto : FullAuditedEntityDto<long>
    {
        public long CarFixAuto { get; set; }
        public string ItemName { get; set; }
        public int ItemCount { get; set; }
        public double ItemAmount { get; set; }
        public int PayType { get; set; }
        public int ItemType { get; set; }
        public string Memo { get; set; }
        public int Seq { get; set; }
        public int CUser { get; set; }
        public DateTime CDT { get; set; }
        public int MUser { get; set; }
        public DateTime MDT { get; set; }
    }
}
