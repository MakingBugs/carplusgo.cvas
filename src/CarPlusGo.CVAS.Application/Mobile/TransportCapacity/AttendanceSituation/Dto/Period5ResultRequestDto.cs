using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.AttendanceSituation.Dto
{
    public class Period5ResultRequestDto
    {
        /// <summary>
        /// 统计周期：1日 2周 3月
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// 订单状态 1上线司机数 2满勤司机数
        /// </summary>
        public int[] DriverStatus { get; set; }
    }
}
