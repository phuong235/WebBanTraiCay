using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models.Common;
using Filter = WebBanHang.Models.Common.Filter;

namespace WebBanHang.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    public class ProductCategoryController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();
        
        // Lấy danh sách loại sản phẩm
        [CustomAuthorize("Admin", "Employee")]
        public ActionResult Index()
        {
            ViewBag.AllertMesssage = TempData["AllertMesssage"] as string;
            var items = db.ProductCategories.ToList();
            return View(items);
        }

        #region Thêm loại
        [CustomAuthorize("Admin", "Employee")]
        public ActionResult Add()
        {
            return View();
        }
        [CustomAuthorize("Admin", "Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.CreatedBy = (string)Session["FullName"];
                model.Alias = Filter.ChuyenCoDauThanhKhongDau(model.Title);
                db.ProductCategories.Add(model);
                db.SaveChanges();
                TempData["AllertMesssage"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Sửa loại
        [CustomAuthorize("Admin", "Employee")]
        public ActionResult Edit(int id)
        {
            var item = db.ProductCategories.Find(id);
            return View(item);
        }
        [CustomAuthorize("Admin", "Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = (string)Session["FullName"];
                model.Alias = Filter.ChuyenCoDauThanhKhongDau(model.Title);
                db.ProductCategories.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["AllertMesssage"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Xóa loại
        [CustomAuthorize("Admin", "Employee")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.ProductCategories.Find(id);
            if (item != null)
            {
                db.ProductCategories.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion
    }
}