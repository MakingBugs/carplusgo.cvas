using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Common.Dto
{
    public class PRInvResultRequestDto : PagedResultRequestDto
    {
        public string InvNo { get; set; }
        public int? PrinvLinkAuto { get; set; }
        public decimal? Amount { get; set; }
        public long? SupplierAuto { get; set; }
    }
}
