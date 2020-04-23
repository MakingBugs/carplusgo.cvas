using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.TakeCarFile.Dto
{
    public class TakeCarResultRequestDto:PagedResultRequestDto
    {
        public long[] Ids { get; set; }
    }
}
