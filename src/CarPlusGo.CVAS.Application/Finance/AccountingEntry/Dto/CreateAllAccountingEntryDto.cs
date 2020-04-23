using CarPlusGo.CVAS.Finance.Enum;

namespace CarPlusGo.CVAS.Finance.Dto
{
    public class CreateAllAccountingEntryDto
    {
        /// <summary>
        /// 摘要
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 会计分录配置名称
        /// </summary>
        public string AccountingEntryName { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 币别
        /// </summary>
        public CurrencyType CurrencyType { get; set; }
    }
}
