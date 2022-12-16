using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models.Common;

namespace WebBanHang.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    public class ProductImagesController : Controller
    {
        // Lấy danh sách ảnh
        private WebBanHangEntities db = new WebBanHangEntities();
        [CustomAuthorize("Admin", "Employee")]
        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            var items = db.ProductImages.Where(x => x.ProductId == id).ToList();
            return View(items);
        }

        #region Thêm Ảnh
        [CustomAuthorize("Admin", "Employee")]
        [HttpPost]
        public ActionResult AddImage(int productId, string url)
        {
            db.ProductImages.Add(new ProductImage
            {
                ProductId = productId,
                Image = url,
                IsDefault = false
            });
            db.SaveChanges();
            return Json(new { Success = true });
        }
        #endregion

        #region Xóa Ảnh
        [CustomAuthorize("Admin", "Employee")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.ProductImages.Find(id);
            if (item != null)
            {
                if (item.IsDefault)
                {
                    var imgDefalut = db.ProductImages.FirstOrDefault(x => x.ProductId == item.ProductId);
                    imgDefalut.IsDefault = true;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
            }
            db.ProductImages.Remove(item);
            db.SaveChanges();
            return Json(new { success = true });
        }
        #endregion

        #region Chọn ảnh làm hiển thị mặc định
        [CustomAuthorize("Admin", "Employee")]
        [HttpPost]
        public ActionResult IsDefault(int id)
        {
            var item = db.ProductImages.Find(id);
            if (item != null)
            {
                UpdateAll(item.ProductId);
                item.IsDefault = !item.IsDefault;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsDefault });
            }
            return Json(new { success = false });
        }


        //cập nhật hết các ảnh của product về isdefault=false
        [CustomAuthorize("Admin", "Employee")]
        private void UpdateAll(int productid)
        {
            var items = db.ProductImages.Where(x => x.ProductId == productid).ToList();
            if (items != null)
            {
                foreach (var item in items)
                {
                    item.IsDefault = false;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        #endregion

    }
}