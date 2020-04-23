using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.UseCarFiles.Dto
{
    [AutoMap(typeof(UseCarAccessory))]
    public class UseCarAccessoryDto : FullAuditedEntityDto<long>
    {
        public long RepositoryOutID { get; set; }//出入库档ID
        public int Type { get; set; }//出入库记录 1出库 2入库
        public long CarAccessoryID { get; set; }//配件编号
        public int Lack { get; set; }//是否缺件 0未缺件 1缺件
        public int dueQty { get; set; }//应收数量
        public int Qty { get; set; }//实收数量
        public string Memo { get; set; }//备注
    }
}
