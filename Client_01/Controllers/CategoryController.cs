using Client.Business;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mvc.Areas.Admin.Models;

namespace Mvc.Areas.Admin.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(TokenExpirationFilter))]
    public class CategoryController : Controller
    {
        private readonly ManageContext _manageContext;
        public CategoryController(ManageContext manageContext)
        {
            _manageContext= manageContext;
        }
        public IActionResult Index()
        {
            var categorys = _manageContext.Categories.OrderBy(x => x.IDParent).ToList();
            return View(categorys);
        }
        public IActionResult Create()
        {
            var listMenu = _manageContext.Categories.ToList();
            var menu = new Category
            {
                ID = 0,
                Name = "Không thuộc menu nào"
            };
            listMenu.Insert(0, menu);
            ViewData["ListMenu"] = new SelectList(listMenu, "ID", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            //var listMenu = _manageContext.Categories.ToList();
            //var menu = new Category
            //{
            //    ID = 0,
            //    Name = "Không thuộc menu nào"
            //};
            //listMenu.Insert(0, menu);
            //ViewData["ListMenu"] = new SelectList(listMenu, "ID", "Name",categoryViewModel.IDParent);
            //if (!ModelState.IsValid) return View(categoryViewModel);
            //try
            //{
            //    var category = new Category();
            //    category.Name = categoryViewModel.Name;
            //    category.Status = categoryViewModel.Status;
            //    category.CreatedDate = DateTime.Now;
            //    category.IDAccount = 1;
            //    category.IDParent = categoryViewModel.IDParent;
            //    category.Link = categoryViewModel.Link;
            //    _manageContext.Categories.Add(category);
            //    await _manageContext.SaveChangesAsync();
            //    return Redirect("/Admin/Category");
            //}
            //catch(Exception ex)
            //{
            //    ViewBag.Message = "Thêm loại sản phẩm thất bại";
            //}
            return View(categoryViewModel);
        }
        public IActionResult Edit(string id)
        {
            var listMenu = _manageContext.Categories.ToList();
            var menu = new Category
            {
                ID = 0,
                Name = "Không thuộc menu nào"
            };
            listMenu.Insert(0, menu);
            ViewData["ListMenu"] = new SelectList(listMenu, "ID", "Name");
            if (string.IsNullOrEmpty(id)) { throw new ArgumentNullException(nameof(id)); }
            try
            {
                //var category = _manageContext.Categories.Find(int.Parse(id));
                //var categoryViewModel = new CategoryViewModel();
                //categoryViewModel.Status = category.Status;
                //categoryViewModel.ID = category.ID;
                //categoryViewModel.IDAccount = category.IDAccount;
                //categoryViewModel.Name = category.Name;
                //categoryViewModel.CreatedDate = category.CreatedDate;
                //categoryViewModel.Link = category.Link;
                return View();
                //return View(categoryViewModel);
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel categoryViewModel)
        {
            //var listMenu = _manageContext.Categories.ToList();
            //var menu = new Category
            //{
            //    ID = 0,
            //    Name = "Không thuộc menu nào"
            //};
            //listMenu.Insert(0, menu);
            //ViewData["ListMenu"] = new SelectList(listMenu, "ID", "Name", categoryViewModel.IDParent); if (!ModelState.IsValid) return View(categoryViewModel);
            //try
            //{
            //    var category = new Category();
            //    category.ID = categoryViewModel.ID;
            //    category.Name = categoryViewModel.Name;
            //    category.Status = categoryViewModel.Status;
            //    category.CreatedDate = categoryViewModel.CreatedDate;
            //    category.IDParent = categoryViewModel.IDParent;
            //    category.IDAccount = categoryViewModel.IDAccount;
            //    category.Link = categoryViewModel.Link;
            //    _manageContext.ChangeTracker.Clear();
            //    _manageContext.Categories.Attach(category);
            //    _manageContext.Entry(category).State = EntityState.Modified;
            //    await _manageContext.SaveChangesAsync();
            //    return Redirect("/Admin/Category");
            //}
            //catch (Exception ex)
            //{
            //    ViewBag.Message = "Chỉnh sửa không thành công";
            //}
            return View(categoryViewModel);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var category = _manageContext.Categories.Find(int.Parse(id));
                _manageContext.Categories.Remove(category);
                await _manageContext.SaveChangesAsync();
                return Json(new
                {
                    status = true
                });
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
        [HttpPost]
        public async Task<JsonResult> ChangeStatus(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var category = _manageContext.Categories.Find(int.Parse(id));
                category.Status = (category.Status == true ? false : true);
                _manageContext.Categories.Attach(category);
                _manageContext.Entry(category).State = EntityState.Modified;
                await _manageContext.SaveChangesAsync();
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}
