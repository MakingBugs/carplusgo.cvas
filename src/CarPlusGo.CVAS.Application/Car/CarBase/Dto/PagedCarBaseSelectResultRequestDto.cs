using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Car.Dto
{
    public class PagedCarBaseSelectResultRequestDto : PagedCarBaseResultRequestDto
    {
        public long? CarBaseAuto { get; set; }
        public string Keyword { get; set; }//模糊查询
        public string MakNo { get; set; }//车牌号
        public string CarNo { get; set; }//车架号
        public string EngNo { get; set; }//发动机号
        public DateTime? MakDtFrom { get; set; }//上牌日期-开始
        public DateTime? MakDtTo { get; set; }//上牌日期-结束
        public DateTime? CarDtFrom { get; set; }//出厂日期-开始
        public DateTime? CarDtTo { get; set; }//出厂日期-结束
        public int? IsBusiness { get; set; }//车险性质别
        public int? Oil { get; set; }//燃油种类
        public long? RepositoryID { get; set; }//仓库
    }
}
