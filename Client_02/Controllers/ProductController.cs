using Client.Business;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(TokenExpirationFilter))]
    public class ProductController : Controller
    {
        private readonly ManageContext _manageContext;
        public ProductController(ManageContext manageContext) {
            _manageContext = manageContext;
        }

        [HttpGet]
        [Route("Product/{id?}")]
        public IActionResult Index(string id)
        {
            try
            {
                var model = _manageContext.Products.SingleOrDefault(x => x.ID == int.Parse(id));
                return View(model);
            }
            catch(Exception ex)
            {
                return Redirect("/Home");
            }
        }
    }
}
