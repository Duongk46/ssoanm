using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Views.Shared.Components.NewProduct
{
    public class NewProductViewComponent : ViewComponent
    {
        private readonly ManageContext _manageContext;
        public NewProductViewComponent (ManageContext manageContext)
        {
            _manageContext= manageContext;
        }
        public IViewComponentResult Invoke()
        {
            var ID_1 = _manageContext.Categories.SingleOrDefault(x => x.Status == true && x.Name == "Ban lãnh đạo viện").ID;
            var ID_2 = _manageContext.Categories.SingleOrDefault(x => x.Status == true && x.Name == "Hội đồng khoa học và đào tạo").ID;
            var model = _manageContext.Products.Where(x => x.Status == true && x.IDCategory != ID_1 && x.IDCategory != ID_2).OrderByDescending(x => x.CreatedDate).Take(6);
            ViewData["ListCategory"] = _manageContext.Categories.Where(x => x.Status == true).ToList();
            return View(model);
        }
    }
}
