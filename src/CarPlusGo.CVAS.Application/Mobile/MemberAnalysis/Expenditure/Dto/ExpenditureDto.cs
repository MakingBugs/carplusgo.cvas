namespace CarPlusGo.CVAS.Mobile.MemberAnalysis.Expenditure.Dto
{
    public class ExpenditureDto
    {
        public int Year { get; set; }
        public string Key { get; set; }
        public long UserCount { get; set; }
        public long OrderCount { get; set; }
        public decimal PayAmount { get; set; }
        public decimal CashPayAmount { get; set; }
    }
}
