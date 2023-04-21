using Microsoft.AspNetCore.Mvc;

namespace Shop.Mvc.Areas.Admin.Views.Shared.Components.NavAdmin
{
    public class NavAdminViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
