using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.AttendanceSituation.Dto
{
    public class Period5Dto
    {
        /// <summary>
        /// 上线司机数
        /// </summary>
        public IOrderedEnumerable<Period5DetailDto> OnlineDriversCount { get; set; }
        /// <summary>
        /// 满勤司机数
        /// </summary>
        public IOrderedEnumerable<Period5DetailDto> WorkFullHoursDriversCount { get; set; }
        /// <summary>
        /// 司机人均日单量
        /// </summary>
        public IOrderedEnumerable<Period5DetailDto> OrdersCount { get; set; }
        /// <summary>
        /// 司机人均日流水
        /// </summary>
        public IOrderedEnumerable<Period5DetailDto> JournalAccount { get; set; }
    }
}
