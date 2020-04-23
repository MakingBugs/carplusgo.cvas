using Microsoft.AspNetCore.Antiforgery;
using CarPlusGo.CVAS.Controllers;

namespace CarPlusGo.CVAS.Web.Host.Controllers
{
    public class AntiForgeryController : CVASControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
