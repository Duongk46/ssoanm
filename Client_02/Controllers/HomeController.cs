using Client.Business;
using Client_02.Models;
using Entities.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ManageContext _db;
        public HomeController(ILogger<HomeController> logger, ManageContext dbContext)
        {
            _logger = logger;
            _db = dbContext;
        }

        public IActionResult Index()
        {
            try
            {
                var ID_1 = _db.Categories.SingleOrDefault(x => x.Status == true && x.Name == "Ban lãnh đạo viện").ID;
                var ID_2 = _db.Categories.SingleOrDefault(x => x.Status == true && x.Name == "Hội đồng khoa học và đào tạo").ID;
                var ID_3 = _db.Categories.SingleOrDefault(x => x.Status == true && x.Name == "Tuyển sinh").ID;
                var ID_4 = _db.Categories.SingleOrDefault(x => x.Status == true && x.Name == "Đào tạo").ID;
                var model = _db.Products.Where(x => x.Status == true && x.IDCategory != ID_1 && x.IDCategory != ID_2).OrderByDescending(x => x.CreatedDate).Take(6);
                ViewData["ListTuyenSinh"] = _db.Categories.Where(x => x.Status == true && x.IDParent == ID_3).ToList();
                ViewData["ListDaoTao"] = _db.Categories.Where(x => x.Status == true && x.IDParent == ID_4).ToList();
                return View(model);
            }
            catch(Exception ex)
            {
                return Redirect("/Home");
            }
            
        }
        [Route("/Logout")]
        public async Task<IActionResult> Logout()
        {
            return new SignOutResult(new[] { "oidc", "Cookies" });
        }
        [Authorize]
        [ServiceFilter(typeof(TokenExpirationFilter))]
        [Route("/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToAction("index", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}