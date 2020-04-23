using Abp.Application.Services.Dto;
using System;

namespace CarPlusGo.CVAS.Car.Dto
{
    public class PagedCarBaseResultRequestDto : PagedResultRequestDto
    {
        //public string Keyword { get; set; }

        //public bool? IsActive { get; set; }
        public long? UseType { get; set; }
        public long? ClasenAuto { get; set; }
        public long? CarBaseAuto { get; set; }
        public string Keyword { get; set; }//模糊查询
        public DateTime? CarDtFrom { get; set; }//出厂日期-开始
        public DateTime? CarDtTo { get; set; }//出厂日期-结束
        public int? IsBusiness { get; set; }//车险性质别
        public int? Oil { get; set; }//燃油种类
        public long? RepositoryID { get; set; }//仓库
    }
}
