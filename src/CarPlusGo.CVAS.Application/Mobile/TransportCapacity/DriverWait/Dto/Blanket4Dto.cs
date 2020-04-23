using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.DriverWait.Dto
{
    public class Blanket4Dto
    {
        /// <summary>
        /// 即时单平均订单完成等待用时
        /// </summary>
        public Double OrderType1SuccessSpendTime { get; set; }
        /// <summary>
        /// 即时单平均订单失败等待用时
        /// </summary>
        public Double OrderType1CancelSpendTime { get; set; }
        /// <summary>
        /// 预约单平均订单完成等待用时
        /// </summary>
        public Double OrderType20SuccessSpendTime { get; set; }
        /// <summary>
        /// 预约单平均订单失败等待用时
        /// </summary>
        public Double OrderType20CancelSpendTime { get; set; }
        /// <summary>
        /// 接机单平均订单完成等待用时
        /// </summary>
        public Double OrderType21SuccessSpendTime { get; set; }
        /// <summary>
        /// 接机单平均订单失败等待用时
        /// </summary>
        public Double OrderType21CancelSpendTime { get; set; }
        /// <summary>
        /// 送机单平均订单完成等待用时
        /// </summary>
        public Double OrderType22SuccessSpendTime { get; set; }
        /// <summary>
        /// 送机单平均订单失败等待用时
        /// </summary>
        public Double OrderType22CancelSpendTime { get; set; }
    }
}
