﻿using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Common.Dto
{
    public class PagedSupplierContectResultRequestDto : PagedResultRequestDto
    {
        public long? SupplierAuto { get; set; }
    }
}
