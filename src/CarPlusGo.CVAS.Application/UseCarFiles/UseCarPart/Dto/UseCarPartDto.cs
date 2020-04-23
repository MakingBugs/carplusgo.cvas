using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CarPlusGo.CVAS.UseCarFiles.Dto
{
    [AutoMap(typeof(UseCarPart))]//出还车车辆部位记录
    public class UseCarPartDto:FullAuditedEntityDto<long>
    {
        public long RepositoryOutID { get; set; }//出入库档ID
        public int Type { get; set; }//出入库记录 1出库 2入库
        public long CarPartID { get; set; }//车辆部位
        public int Status { get; set; }//车辆提领状态
        public string Memo { get; set; }//备注
    }
}
