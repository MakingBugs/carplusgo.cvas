using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.RepositoryOutCar.Dto
{
    public class RepositoryOutResultRequestDto:PagedResultRequestDto
    {
        public string MakNo { get; set; }//车牌号
        public long? OutRepositoryID { get; set; }//出库仓库
        public long? InRepositoryID { get; set; }//入库仓库
        public DateTime? OutDateForm { get; set; }//出库日期
        public DateTime? OutDateTo { get; set; }//出库日期
        public DateTime? InDateForm { get; set; }//入库日期
        public DateTime? InDateTo { get; set; }//入库日期
        public long? Status { get; set; }//出库状态1608
    }
}
