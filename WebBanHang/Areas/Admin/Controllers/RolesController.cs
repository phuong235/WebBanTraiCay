using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models.Common;

namespace WebBanHang.Areas.Admin.Controllers
{
    /*[CustomAuthenticationFilter]*/
    public class RolesController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();

        #region Lấy danh sách quyền
        [CustomAuthorize("Admin")]
        public ActionResult Index()
        {
            ViewBag.AllertMesssage = TempData["AllertMesssage"] as string;
            var items = db.Roles.ToList();
            return View(items);
        }
        #endregion

        #region Thêm quyền
        [CustomAuthorize("Admin")]
        public ActionResult Add()
        {
            return View();
        }


        [CustomAuthorize("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Role model)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(model);
                db.SaveChanges();
                TempData["AllertMesssage"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Sửa quyền
        [CustomAuthorize("Admin")]
        public ActionResult Edit(int id)
        {
            var item = db.Roles.Find(id);
            return View(item);
        }

        [CustomAuthorize("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role model)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["AllertMesssage"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Xóa quyền
        [CustomAuthorize("Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Roles.Find(id);
            if (item != null)
            {
                db.Roles.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion
    }
}