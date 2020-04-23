using System;

namespace CarPlusGo.CVAS.Mobile.Analysis.Dto
{
    public class PerformanceDetailDto
    {
        public int Year { get; set; }
        public string Key { get; set; }
        public int OrderCount { get; set; }
        public Decimal OrderAmount { get; set; }
        public Decimal PayAmount { get; set; }
    }
}
