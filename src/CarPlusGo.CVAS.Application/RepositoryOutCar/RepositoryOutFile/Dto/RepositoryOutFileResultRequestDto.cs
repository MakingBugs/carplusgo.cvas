using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.RepositoryOutCar.Dto
{
    public class RepositoryOutFileResultRequestDto: PagedResultRequestDto
    {
        public long? RepositoryOutID { get; set; }//出入库档ID
        public int? Type { get; set; }//出入库记录 1.出庫 2.入庫
        public long[] Ids { get; set; }
    }
}
