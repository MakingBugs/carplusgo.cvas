using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.TakeCarFile.Dto
{
    public class TakeCarApplyResultRequestDto : PagedResultRequestDto
    {
        public DateTime? TakeDateForm { get; set; }//提领日期
        public DateTime? TakeDateTo { get; set; }//提领日期
        public long? Status { get; set; }//提领申请状态
        public bool? IsStatus { get; set; }//是否显示取消领用、完成领用
    }
}
