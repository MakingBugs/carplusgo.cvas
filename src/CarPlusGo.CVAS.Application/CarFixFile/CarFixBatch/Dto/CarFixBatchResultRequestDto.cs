using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile.Dto
{
    public class CarFixBatchResultRequestDto : PagedResultRequestDto
    {
        public DateTime? PAyDT { get; set; }
        public long? SupplierAuto { get; set; }
        public long? CarFixBatchTNO { get; set; }
    }
}
