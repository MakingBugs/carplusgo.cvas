using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.PassengerWait.Dto
{
    public class ByTheHour3ResultRequestDto
    {
        /// <summary>
        /// 所选日期
        /// </summary>
        public DateTime[] DayList { get; set; }
        /// <summary>
        /// 订单类型：1及时 2预约 3接机 4送机 0所有
        /// </summary>
        public int[] OrderType { get; set; }
        /// <summary>
        /// 订单状态 1下单成功 2乘客取消下单
        /// </summary>
        public int[] OrderStatus { get; set; }
    }
}
