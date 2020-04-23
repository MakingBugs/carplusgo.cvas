namespace CarPlusGo.CVAS.Mobile.Analysis.Dto
{
    public class OrderInfoDetailDto
    {
        public int Year { get; set; }
        public string Key { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal PayAmount { get; set; }
        public decimal Distance { get; set; }
        public decimal Duration { get; set; }
        public decimal DriverEvaluation { get; set; }
        public decimal CarEvaluation { get; set; }
    }
}
