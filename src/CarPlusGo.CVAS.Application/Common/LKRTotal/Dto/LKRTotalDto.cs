using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(LKRTotal))]
    public class LKRTotalDto : EntityDto<long>
    {
        public string LKRName { get; set; }
        public string LKRBank { get; set; }
        public int LKRBankType { get; set; }
        public BankType BankType { get; set; }
        public string LKRAcount { get; set; }
        public int IsOn { get; set; }
        public long CUser { get; set; }
        public DateTime CDT { get; set; }
        public int? MUser { get; set; }
        public DateTime? MDT { get; set; }
    }
}
