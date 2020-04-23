using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.MemberAnalysis.RechargeGeneral.Dto
{
    public class RechargeTendencyDto
    {
        // <summary>
        /// 充值用户数
        /// </summary>
        public IEnumerable<RechargeTendencyDetailDto> RechargeUsers { get; set; }
        /// <summary>
        /// 充值订单笔数
        /// </summary>
        public IEnumerable<RechargeTendencyDetailDto> RechargeOrders { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public IEnumerable<RechargeTendencyDetailDto> RechargeAmount { get; set; }
        /// <summary>
        /// 赠送金额
        /// </summary>
        public IEnumerable<RechargeTendencyDetailDto> PresenterAmount { get; set; }
    }
}
