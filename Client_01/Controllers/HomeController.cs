using Client.Business;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ManageContext _manageContext;
        public HomeController(ManageContext manageContext)
        {
            _manageContext = manageContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        [ServiceFilter(typeof(TokenExpirationFilter))]
        [HttpGet("/login")]
        public IActionResult Login()
        {
            return RedirectToAction("index", "Home");
        }
        [Authorize]
        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            return new SignOutResult(new[] { "oidc", "Cookies" });
        }
    }
}
