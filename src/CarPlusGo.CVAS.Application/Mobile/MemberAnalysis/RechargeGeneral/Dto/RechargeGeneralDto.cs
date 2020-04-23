using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.MemberAnalysis.RechargeGeneral.Dto
{
    public class RechargeGeneralDto
    {
        /// <summary>
        /// 充值用户数
        /// </summary>
        public int RechargeUsers { get; set; }
        /// <summary>
        /// 充值订单笔数
        /// </summary>
        public int RechargeOrders { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public Double RechargeAmount { get; set; }
        /// <summary>
        /// 赠送金额
        /// </summary>
        public Double PresenterAmount { get; set; }
        /// <summary>
        /// 余额账户数
        /// </summary>
        public int BalanceAmounts { get; set; }
        /// <summary>
        /// 账户总余额
        /// </summary>
        public Double TotalBalance { get; set; }
        /// <summary>
        /// 本金余额
        /// </summary>
        public Double CapitalBalance { get; set; }
        /// <summary>
        /// 赠送余额
        /// </summary>
        public Double PresenterBalance { get; set; }
    }
}
