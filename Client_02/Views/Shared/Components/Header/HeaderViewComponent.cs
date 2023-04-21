using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Views.Shared.Component.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ManageContext _manageContext;
        public HeaderViewComponent(ManageContext manageContext)
        {
            _manageContext= manageContext;
        }
        public IViewComponentResult Invoke()
        {
            var model = _manageContext.Categories.Where(x => x.Status == true).ToList();
            return View(model);
        }
    }
}
