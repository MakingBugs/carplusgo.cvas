using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.PassengerWait.Dto
{
    public class Period3Dto
    {
        /// <summary>
        /// 全部-成功
        /// </summary>
        //public IOrderedEnumerable<Period3DetailDto> AllSucces { get; set; }
        /// <summary>
        /// 全部-失败
        /// </summary>
        //public IOrderedEnumerable<Period3DetailDto> AllCancel { get; set; }
        /// <summary>
        /// 即时-成功
        /// </summary>
        public IOrderedEnumerable<Period3DetailDto> ForthwithSucces { get; set; }
        /// <summary>
        /// 预约-成功
        /// </summary>
        public IOrderedEnumerable<Period3DetailDto> OrderSucces { get; set; }
        /// <summary>
        /// 接机-成功
        /// </summary>
        public IOrderedEnumerable<Period3DetailDto> AirportPickupSuccess { get; set; }
        /// <summary>
        /// 送机-成功
        /// </summary>
        public IOrderedEnumerable<Period3DetailDto> AirportDropOffSucces { get; set; }
        /// <summary>
        /// 即时-取消
        /// </summary>
        public IOrderedEnumerable<Period3DetailDto> ForthwithCancel { get; set; }
        /// <summary>
        /// 预约-取消
        /// </summary>
        public IOrderedEnumerable<Period3DetailDto> OrderCancel { get; set; }
        /// <summary>
        /// 接机-取消
        /// </summary>
        public IOrderedEnumerable<Period3DetailDto> AirportPickupCancel { get; set; }
        /// <summary>
        /// 送机-取消
        /// </summary>
        public IOrderedEnumerable<Period3DetailDto> AirportDropOffCancel { get; set; }
    }
}
