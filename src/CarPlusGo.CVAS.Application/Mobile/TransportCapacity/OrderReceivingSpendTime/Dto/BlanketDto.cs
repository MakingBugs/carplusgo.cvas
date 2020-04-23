using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.OrderReceivingSpendTime.Dto
{
    public class BlanketDto
    {
        /// <summary>
        /// 平均接单用时
        /// </summary>
        public Double OrderReceivingSpendTime { get; set; }
        /// <summary>
        /// 平均乘客取消下单用时
        /// </summary>
        public Double CancelOrderSpendTime { get; set; }
        /// <summary>
        /// 即时单平均下单成功用时
        /// </summary>
        public Double OrderType1SuccessSpendTime { get; set; }
        /// <summary>
        /// 即时单平均下单失败用时
        /// </summary>
        public Double OrderType1FailSpendTime { get; set; }
        /// <summary>
        /// 预约单平均下单成功用时
        /// </summary>
        public Double OrderType20SuccessSpendTime { get; set; }
            /// <summary>
            /// 预约单平均下单失败用时
            /// </summary>
        public Double OrderType20FailSpendTime { get; set; }
        /// <summary>
        /// 接机单平均下单成功用时
        /// </summary>
        public Double OrderType21SuccessSpendTime { get; set; }
        /// <summary>
        /// 接机单平均下单失败用时
        /// </summary>
        public Double OrderType21FailSpendTime { get; set; }
        /// <summary>
        /// 送机单平均下单成功用时
        /// </summary>
        public Double OrderType22SuccessSpendTime { get; set; }
        /// <summary>
        /// 送机单平均下单失败用时
        /// </summary>
        public Double OrderType22FailSpendTime { get; set; }
    }
}
