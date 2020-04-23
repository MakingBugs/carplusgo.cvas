using System;
using System.ComponentModel.DataAnnotations;

namespace CarPlusGo.CVAS.Mobile.MemberAnalysis.Expenditure.Dto
{
    public class ExpenditureOverviewResultRequestDto
    {
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }
    }
}
