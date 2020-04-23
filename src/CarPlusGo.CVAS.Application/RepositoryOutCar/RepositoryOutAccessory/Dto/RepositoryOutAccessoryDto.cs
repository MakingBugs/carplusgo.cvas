using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Car.Dto;

namespace CarPlusGo.CVAS.RepositoryOutCar.Dto
{
    [AutoMap(typeof(RepositoryOutAccessory))]//出入库车辆配件记录
    public class RepositoryOutAccessoryDto:FullAuditedEntityDto<long>
    {
        public long RepositoryOutID { get; set; }//出入库档ID
        public int Type { get; set; }//出入库记录 1出库 2入库
        public long CarAccessoryID { get; set; }//配件编号
        public int Lack { get; set; }//是否缺件 0未缺件 1缺件
        public int dueQty { get; set; }//应收数量
        public int Qty { get; set; }//实收数量
        public string Memo { get; set; }//备注
        public RepositoryOutDto RepositoryOut { get; set; }
        public CarAccessoryDto CarAccessory { get; set; }
    }
}
