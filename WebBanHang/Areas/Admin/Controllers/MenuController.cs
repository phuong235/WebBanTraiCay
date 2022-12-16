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
    public class MenuController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();
        
        // Lấy danh sách menu
        [CustomAuthorize("Admin")]
        public ActionResult Index()
        {
            ViewBag.AllertMesssage = TempData["AllertMesssage"] as string;
            var items = db.Menus;
            return View(items);
        }

        #region Thêm Menu
        [CustomAuthorize("Admin")]
        public ActionResult Add()
        {
            return View();
        }

        [CustomAuthorize("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Menu model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = (string)Session["FullName"];
                model.ModifiedDate = DateTime.Now;
                model.Alias = Filter.ChuyenCoDauThanhKhongDau(model.Title);
                db.Menus.Add(model);
                db.SaveChanges();
                TempData["AllertMesssage"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Sửa Menu
        [CustomAuthorize("Admin")]
        public ActionResult Edit(int id)
        {
            var item = db.Menus.Find(id);
            return View(item);
        }

        [CustomAuthorize("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu model)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = (string)Session["FullName"];
                model.Alias = Filter.ChuyenCoDauThanhKhongDau(model.Title);
                db.Entry(model).Property(x => x.Title).IsModified = true;
                db.Entry(model).Property(x => x.Alias).IsModified = true;
                db.Entry(model).Property(x => x.Position).IsModified = true;
                db.Entry(model).Property(x => x.ModifiedDate).IsModified = true;
                db.Entry(model).Property(x => x.ModifiedBy).IsModified = true;
                db.SaveChanges();
                TempData["AllertMesssage"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Xóa Menu
        [CustomAuthorize("Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Menus.Find(id);
            if (item != null)
            {
                db.Menus.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion
    }
}