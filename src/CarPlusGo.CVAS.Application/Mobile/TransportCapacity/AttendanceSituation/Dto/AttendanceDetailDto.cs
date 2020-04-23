using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.AttendanceSituation.Dto
{
    public class AttendanceDetailDto
    {
        /// <summary>
        /// 队伍名称
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// 在线司机数
        /// </summary>
        public int OnlineDriversCount { get; set; }
        /// <summary>
        /// 满勤司机数
        /// </summary>
        public int WorkFullHoursDriversCount { get; set; }
        /// <summary>
        /// 司机人均日单量
        /// </summary>
        public Double OrdersCount { get; set; }
        /// <summary>
        /// 司机人均日流水
        /// </summary>
        public Double JournalAccount { get; set; }
        /// <summary>
        /// 车队司机数
        /// </summary>
        public Double TeamDrivers { get; set; }
    }
}
