using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ManageContext _manageContext;
        public CategoryController(ManageContext manageContext) {
            _manageContext = manageContext;
        }
        [HttpGet]
        [Route("Category/{id?}")]
        public IActionResult Index(string id,int page = 1,int pageSize = 4)
        {
            try
            {
                var model = _manageContext.Products.Where(x => x.IDCategory == int.Parse(id) && x.Status == true).OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (model.Count() > 0)
                {
                    var listProduct = _manageContext.Products.Where(x => x.IDCategory == int.Parse(id) && x.Status == true).ToPagedList(page, pageSize);
                    ViewData["Paginations"] = listProduct;

                }
                else
                {
                    ViewData["Paginations"] = null;
                }
                ViewBag.ID = id;
                return View(model);
            }
            catch(Exception ex)
            {
                return Redirect("/Home");
            }
            return View();
        }
    }
}
