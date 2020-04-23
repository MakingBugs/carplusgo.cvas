using System;
using System.Collections.Generic;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.OrderMap.Dto
{
    public class HeatDataResultRequestDto
    {
        public List<DateTime> Dates { get; set; }
        public List<DateTime> Times { get; set; }
        public List<InputOrderType> OrderType { get; set; }
        public List<InputOrderStatus> OrderStatus { get; set; }
        public InputPlaceType PlaceType { get; set; }
        public int SegmentationNumber { get; set; } = 50;
    }
}
