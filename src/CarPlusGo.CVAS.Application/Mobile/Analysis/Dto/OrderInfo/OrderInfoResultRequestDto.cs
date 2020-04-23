using System;
using System.ComponentModel.DataAnnotations;

namespace CarPlusGo.CVAS.Mobile.Analysis.Dto
{
    public class OrderInfoResultRequestDto
    {
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }
    }
}
