using Client.Business;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mvc.Areas.Admin.Models;
using System.IO;
using X.PagedList;

namespace Mvc.Areas.Admin.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(TokenExpirationFilter))]
    public class ProductController : Controller
    {
        private readonly ManageContext _manageContext;
        public ProductController(ManageContext manageContext)
        {
            _manageContext = manageContext;
        }
        public IActionResult Index(int page=1,int pageSize = 5)
        {
            var model = _manageContext.Products.Skip((page - 1)*pageSize).Take(pageSize).ToList();
            var listProduct = _manageContext.Products.ToPagedList(page, pageSize);
            ViewData["ListCategory"] = _manageContext.Categories.ToList();
            if (listProduct == null)
                ViewData["Paginations"] = null;
            else
                ViewData["Paginations"] = listProduct;
            return View(model);
        }
        public IActionResult Create()
        {
            ViewData["IDCategory"] = new SelectList(_manageContext.Categories.ToList(), "ID", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            ViewData["IDCategory"] = new SelectList(_manageContext.Categories.ToList(), "ID", "Name", productViewModel.IDCategory);
            if (!ModelState.IsValid) return View(productViewModel);
            try
            {
                if (productViewModel.FileContent == null)
                {
                    ViewBag.Message = "Vui lòng chọn hình ảnh";
                }
                else
                {
                    string fileName = productViewModel.FileContent.FileName;
                    fileName = Path.GetFileName(fileName);
                    string uploadPaths = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//uploadFiles", fileName);
                    var stream = new FileStream(uploadPaths, FileMode.Create);
                    await productViewModel.FileContent.CopyToAsync(stream);
                    stream.Dispose();
                    var product = new Product();
                    product.Name = productViewModel.Name;
                    product.FileContent = productViewModel.FileContent.FileName;
                    product.Status = productViewModel.Status;
                    product.Description = productViewModel.Description;
                    product.IDCategory = productViewModel.IDCategory;
                    product.CreatedDate = DateTime.Now;
                    _manageContext.Products.Add(product);
                    await _manageContext.SaveChangesAsync();
                    return Redirect("/Admin/Product");
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Tạo bài viết thất bại";
            }
            return View();
        }
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) { throw new ArgumentNullException(nameof(id)); }
            try
            {
                var productViewModel = new ProductViewModel();
                var product = _manageContext.Products.Find(int.Parse(id));
                productViewModel.Name = product.Name;
                productViewModel.ID = product.ID;
                productViewModel.Status = product.Status;
                productViewModel.Description = product.Description;
                productViewModel.CreatedDate = product.CreatedDate;
                ViewBag.Link = product.FileContent;
                ViewData["IDCategory"] = new SelectList(_manageContext.Categories.ToList(), "ID", "Name", product.IDCategory);
                return View(productViewModel);
            }
            catch (Exception ex)
            {
                return Redirect("/Admin/Product");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel productViewModel)
        {
            var productOld = _manageContext.Products.Find(productViewModel.ID);
            ViewBag.Link = productOld.FileContent;
            ViewData["IDCategory"] = new SelectList(_manageContext.Categories.ToList(), "ID", "Name", productViewModel.IDCategory);
            if (!ModelState.IsValid) return View(productViewModel);
            try
            {
                var product = new Product();
                if (productViewModel.FileContent != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//uploadFiles", productOld.FileContent);
                    System.IO.File.Delete(path);
                    string fileName = productViewModel.FileContent.FileName;
                    fileName = Path.GetFileName(fileName);
                    string uploadPaths = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//uploadFiles", fileName);
                    var stream = new FileStream(uploadPaths, FileMode.Create);
                    await productViewModel.FileContent.CopyToAsync(stream);
                    stream.Dispose();
                    product.FileContent = productViewModel.FileContent.FileName;
                }
                else
                {
                    product.FileContent = productOld.FileContent;
                }
                product.ID = productViewModel.ID;
                product.Name = productViewModel.Name;
                product.Status = productViewModel.Status;
                product.Description = productViewModel.Description;
                product.IDCategory = productViewModel.IDCategory;
                product.CreatedDate = productViewModel.CreatedDate;
                _manageContext.ChangeTracker.Clear();
                _manageContext.Products.Attach(product);
                _manageContext.Entry(product).State = EntityState.Modified;
                await _manageContext.SaveChangesAsync();
                return Redirect("/Admin/Product");
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Thay đổi bài viết thất bại";
            }
            return View(productViewModel);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var product = _manageContext.Products.Find(int.Parse(id));
                _manageContext.Products.Remove(product);
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
        [HttpPost]
        public async Task<JsonResult> ChangeStatus(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var product = _manageContext.Products.Find(int.Parse(id));
                product.Status = (product.Status == true ? false : true);
                _manageContext.Products.Attach(product);
                _manageContext.Entry(product).State = EntityState.Modified;
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
