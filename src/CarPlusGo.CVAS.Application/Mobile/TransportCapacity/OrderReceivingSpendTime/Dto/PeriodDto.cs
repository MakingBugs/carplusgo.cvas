using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.OrderReceivingSpendTime.Dto
{
    public class PeriodDto
    {
        /// <summary>
        /// 全部-成功
        /// </summary>
        public IOrderedEnumerable<PeriodDetailDto> AllSucces { get; set; }
        /// <summary>
        /// 全部-失败
        /// </summary>
        public IOrderedEnumerable<PeriodDetailDto> AllCancel { get; set; }
        /// <summary>
        /// 即时-成功
        /// </summary>
        public IOrderedEnumerable<PeriodDetailDto> ForthwithSucces { get; set; }
        /// <summary>
        /// 预约-成功
        /// </summary>
        public IOrderedEnumerable<PeriodDetailDto> OrderSucces { get; set; }
        /// <summary>
        /// 接机-成功
        /// </summary>
        public IOrderedEnumerable<PeriodDetailDto> AirportPickupSuccess { get; set; }
        /// <summary>
        /// 送机-成功
        /// </summary>
        public IOrderedEnumerable<PeriodDetailDto> AirportDropOffSucces { get; set; }
        /// <summary>
        /// 即时-取消
        /// </summary>
        public IOrderedEnumerable<PeriodDetailDto> ForthwithCancel { get; set; }
        /// <summary>
        /// 预约-取消
        /// </summary>
        public IOrderedEnumerable<PeriodDetailDto> OrderCancel { get; set; }
        /// <summary>
        /// 接机-取消
        /// </summary>
        public IOrderedEnumerable<PeriodDetailDto> AirportPickupCancel { get; set; }
        /// <summary>
        /// 送机-取消
        /// </summary>
        public IOrderedEnumerable<PeriodDetailDto> AirportDropOffCancel { get; set; }
    }
}
