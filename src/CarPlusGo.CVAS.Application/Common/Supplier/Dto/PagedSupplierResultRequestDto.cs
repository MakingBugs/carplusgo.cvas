using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace CarPlusGo.CVAS.Common.Dto
{
    public class PagedSupplierResultRequestDto : PagedResultRequestDto
    {
        public int[] SupplierTypes { get; set; }
        public string Key { get; set; }
        public long? SupplierAuto { get; set; }
    }
}
