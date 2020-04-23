using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Car.Dto
{
    public class PagedSelectUsableCarByJurisdictionDto : PagedCarBaseResultRequestDto
    {
        public long? RepositoryId { get; set; }//仓库
        public string MakNo { get; set; }//车牌号
    }
}
