using Microsoft.AspNetCore.Mvc;

namespace Shop.Mvc.Areas.Admin.Views.Shared.Components.MenuAdmin
{
    public class MenuAdminViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
