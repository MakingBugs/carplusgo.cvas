using System.ComponentModel.DataAnnotations;

namespace CarPlusGo.CVAS.Configuration.Dto
{
    public class ChangeUiThemeInput
    {
        [Required]
        [StringLength(32)]
        public string Theme { get; set; }
    }
}
