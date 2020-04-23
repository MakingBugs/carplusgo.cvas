using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CarPlusGo.CVAS.UseCarFiles.Dto
{
    [AutoMap(typeof(UseCarFile))]
    public class UseCarFileDto : FullAuditedEntityDto<long>
    {
        public long RepositoryOutID { get; set; }//出入库档ID
        public int Type { get; set; }//出入库记录 1.出庫 2.入庫
        public string FileName { get; set; }//文件名称
        public int Status { get; set; }//状态
        public string Path { get; set; }//url
    }
}
