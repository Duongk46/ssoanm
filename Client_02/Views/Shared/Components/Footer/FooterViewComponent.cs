using Microsoft.AspNetCore.Mvc;

namespace Mvc.Views.Shared.Component
{
    public class FooterViewComponent : ViewComponent
    {
        public FooterViewComponent()
        {

        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
