using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Common.Dto
{
    public class PagedVEmpResultRequestDto : PagedResultRequestDto
    {
        public string DepCode { get; set; }
    }
}
