using CarPlusGo.CVAS.Mobile.TShareBank.Enum;
using System;

namespace CarPlusGo.CVAS.Mobile.Analysis.Dto
{
    public class HomeDataResultRequestDto
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public Unit Unit { get; set; }
        public string Time { get; set; }
    }
}
