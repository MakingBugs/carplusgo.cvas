using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.ArriveSpendTime.Dto
{
    public class Blanket2Dto
    {
        /// <summary>
        /// 订单成功预估时间
        /// </summary>
        public Double SuccessForecastTime { get; set; }
        /// <summary>
        /// 订单成功实际时间
        /// </summary>
        public Double SuccessPracticalTime { get; set; }
        /// <summary>
        /// 订单成功预估距离
        /// </summary>
        public Double SuccessForecastDistance { get; set; }
        /// <summary>
        /// 订单成功实际距离
        /// </summary>
        public Double SuccessPracticalDistance { get; set; }
        /// <summary>
        /// 订单取消预估时间
        /// </summary>
        public Double CancelForecastTime { get; set; }
        /// <summary>
        /// 订单取消实际时间
        /// </summary>
        public Double CancelPracticalTime { get; set; }
        /// <summary>
        /// 订单取消预估距离
        /// </summary>
        public Double CancelForecastDistance { get; set; }
        /// <summary>
        /// 订单取消实际距离
        /// </summary>
        public Double CancelPracticalDistance { get; set; }
    }
}
