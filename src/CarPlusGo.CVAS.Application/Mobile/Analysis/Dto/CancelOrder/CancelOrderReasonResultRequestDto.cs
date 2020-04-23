using System;
using System.ComponentModel.DataAnnotations;

namespace CarPlusGo.CVAS.Mobile.Analysis.Dto
{
    public class CancelOrderReasonResultRequestDto
    {
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }

        public bool IsUnique { get; set; }

        public bool? IsAccepted { get; set; }
    }
}
