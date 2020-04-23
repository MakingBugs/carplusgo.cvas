using Abp.AutoMapper;
using CarPlusGo.CVAS.Authentication.External;

namespace CarPlusGo.CVAS.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
