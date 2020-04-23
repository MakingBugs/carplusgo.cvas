using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Common.Dto
{
    public class PagedBankDetailResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public int? BankType { get; set; }
    }
}
