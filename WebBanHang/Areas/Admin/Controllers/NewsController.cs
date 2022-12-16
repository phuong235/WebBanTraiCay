using System;
using System.Linq;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models.Common;
using Filter = WebBanHang.Models.Common.Filter;

namespace WebBanHang.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    public class NewsController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();
        // Lấy danh sách Tin Tức
        [CustomAuthorize("Admin")]
        public ActionResult Index()
        {
            var items = db.News;
            ViewBag.AllertMesssage = TempData["AllertMesssage"] as string;
            return View(items);
        }

        #region Thêm tin
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.CreatedBy = (string)Session["FullName"];
                model.Alias = Filter.ChuyenCoDauThanhKhongDau(model.Title);
                db.News.Add(model);
                db.SaveChanges();
                TempData["AllertMesssage"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Sửa tin
        public ActionResult Edit(int id)
        {
            var item = db.News.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = (string)Session["FullName"];
                model.Alias = Filter.ChuyenCoDauThanhKhongDau(model.Title);
                db.News.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["AllertMesssage"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Xóa tin
        // Xóa 1 tin
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                db.News.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        // Xóa nhiều tin
        [HttpPost]
        public ActionResult DeleteAll(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var items = id.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.News.Find(Convert.ToInt32(item));
                        db.News.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion

        #region Cho phép tin nào hiển thị
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        #endregion

    }
}