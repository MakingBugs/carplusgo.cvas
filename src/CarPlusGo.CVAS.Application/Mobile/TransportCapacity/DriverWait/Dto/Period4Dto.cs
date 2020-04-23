using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.DriverWait.Dto
{
    public class Period4Dto
    {
        /// <summary>
        /// 全部-成功
        /// </summary>
        //public IOrderedEnumerable<Period4DetailDto> AllSucces { get; set; }
        /// <summary>
        /// 全部-失败
        /// </summary>
        //public IOrderedEnumerable<Period4DetailDto> AllCancel { get; set; }
        /// <summary>
        /// 即时-成功
        /// </summary>
        public IOrderedEnumerable<Period4DetailDto> ForthwithSucces { get; set; }
        /// <summary>
        /// 预约-成功
        /// </summary>
        public IOrderedEnumerable<Period4DetailDto> OrderSucces { get; set; }
        /// <summary>
        /// 接机-成功
        /// </summary>
        public IOrderedEnumerable<Period4DetailDto> AirportPickupSuccess { get; set; }
        /// <summary>
        /// 送机-成功
        /// </summary>
        public IOrderedEnumerable<Period4DetailDto> AirportDropOffSucces { get; set; }
        /// <summary>
        /// 即时-取消
        /// </summary>
        public IOrderedEnumerable<Period4DetailDto> ForthwithCancel { get; set; }
        /// <summary>
        /// 预约-取消
        /// </summary>
        public IOrderedEnumerable<Period4DetailDto> OrderCancel { get; set; }
        /// <summary>
        /// 接机-取消
        /// </summary>
        public IOrderedEnumerable<Period4DetailDto> AirportPickupCancel { get; set; }
        /// <summary>
        /// 送机-取消
        /// </summary>
        public IOrderedEnumerable<Period4DetailDto> AirportDropOffCancel { get; set; }
    }
}
