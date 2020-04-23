using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile.Dto
{
    public class CarFixResultRequestDto : PagedResultRequestDto
    {
        public long? Id { get; set; }
        public string MakNo { get; set; }
        public long? CarBaseAuto { get; set; }
        public DateTime? Cdt { get; set; }
        public long? SupplierAuto { get; set; }
        public long? CarFixBatchAuto { get; set; }
        public long[] Status { get; set; }
    }
}
