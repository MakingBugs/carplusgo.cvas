using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.AttendanceSituation.Dto
{
    public class Blanket5Dto
    {
        /// <summary>
        /// 在岗司机数
        /// </summary>
        public int DriversCount { get; set; }
        /// <summary>
        /// 日均上线司机数
        /// </summary>
        public Double OnlineDriversCount { get; set; }
        /// <summary>
        /// 日均未上线司机数
        /// </summary>
        public Double OfflineDriversCount { get; set; }
        /// <summary>
        /// 日均满勤司机数
        /// </summary>
        public Double WorkFullHoursDriversCount { get; set; }
        /// <summary>
        /// 日均未满勤司机数
        /// </summary>
        public Double UnWorkFullHoursDriversCount { get; set; }
        /// <summary>
        /// 司机人均日单量
        /// </summary>
        public Double OrdersCount { get; set; }
        /// <summary>
        /// 司机人均日流水
        /// </summary>
        public Double JournalAccount { get; set; }
    }
}
