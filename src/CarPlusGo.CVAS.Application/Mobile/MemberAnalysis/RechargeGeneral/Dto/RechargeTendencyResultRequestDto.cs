using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.MemberAnalysis.RechargeGeneral.Dto
{
    public class RechargeTendencyResultRequestDto
    {
        /// <summary>
        /// 统计周期：1日 2周 3月
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// 业绩指标类型 1充值用户数 2充值订单数 3充值金额 4赠送金额
        /// </summary>
        public int[] Types { get; set; }
    }
}
