using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.PassengerWait.Dto
{
    public class Period3ResultRequestDto
    {
        /// <summary>
        /// 统计周期：1日 2周 3月
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// 订单类型：1及时 2预约 3接机 4送机
        /// </summary>
        public int[] OrderType { get; set; }
        /// <summary>
        /// 订单状态 1下单成功 2乘客取消下单
        /// </summary>
        public int[] OrderStatus { get; set; }
    }
}
