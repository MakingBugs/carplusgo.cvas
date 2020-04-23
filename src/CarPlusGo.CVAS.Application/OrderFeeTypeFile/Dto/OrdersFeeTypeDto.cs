using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.OrdersFeeTypeFile.Dto
{
    [AutoMap(typeof(OrdersFeeType))]
    public class OrdersFeeTypeDto : EntityDto<long>
    {
        public long IncAuto { get; set; }
        public Inc Inc { get; set; }
        public int FeeTypeAuto { get; set; }
        public ItemCode FeeType { get; set; } 
        public decimal FeeAmt { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
        public int? IsLock { get; set; } 
        public string OrdersFeeTypeName { get; set; }
    }
}
