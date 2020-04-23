using System;
using System.Collections.Generic;

namespace CarPlusGo.CVAS.Insure.Dto
{
    public class SurrenderDto
    {
        /// <summary>
        /// 批单号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 保单Id
        /// </summary>
        public List<long> InsurancePolicyIds { get; set; }
    }
}
