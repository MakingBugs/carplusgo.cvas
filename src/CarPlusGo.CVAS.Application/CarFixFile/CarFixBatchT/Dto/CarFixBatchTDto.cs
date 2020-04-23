using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile.Dto
{
    [AutoMap(typeof(CarFixBatchT))]
    public class CarFixBatchTDto : FullAuditedEntityDto<long>
    {
        public long? CarFixBatchTNO { get; set; }
        public decimal Amt { get; set; }
        public int Status { get; set; }
        public int RequestStatus { get; set; }
        public int? PayMode { get; set; }
        public int? IsPAy { get; set; }
        public DateTime? PAyDT { get; set; }
        public long CUser { get; set; }
        public DateTime CDT { get; set; }
        public long? MUser { get; set; }
        public DateTime? MDT { get; set; }
    }
}
