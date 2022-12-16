/*using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanHang.Data;
using WebBanHang.Models.EF;


namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private WebBanHangFinalContext db = new WebBanHangFinalContext();
        // GET: Admin/Role
        public ActionResult Index()
        {
            ViewBag.AllertMesssage = TempData["AllertMesssage"] as string;
            var items = db.Roles.ToList();
            return View(items);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManager.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var item = db.Roles.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManager.Update(model);
                TempData["AllertMesssage"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(string id)
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
    }
}*/