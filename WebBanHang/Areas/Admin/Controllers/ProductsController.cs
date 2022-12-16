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
    public class ProductsController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();

        #region Lấy danh sách sản phẩm
        [CustomAuthorize("Admin", "Employee")]
        public ActionResult Index()
        {
            var items = db.Products;
            ViewBag.NumberOfCategories = db.ProductCategories.Count();
            ViewBag.TotalProducts = db.Products.Count();
            var i = 0;
            var l = 0;
            foreach (var item in items)
            {
                if (item.Quantity == 0)
                {
                    i++;
                }
                if (item.Quantity < 10 && item.Quantity > 0)
                {
                    l++;
                }
            }
            // Sản phẩm hết hàng
            ViewBag.ProductOutStock = i;
            ViewBag.ProductNearOutStock = l;
            ViewBag.AllertMesssage = TempData["AllertMesssage"] as string;
            return View(items);
        }
        #endregion

        #region Thêm sản phẩm
        [CustomAuthorize("Admin", "Employee")]
        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View();
        }

        [CustomAuthorize("Admin", "Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.ProductImages.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true,
                            });
                        }
                        else
                        {
                            model.ProductImages.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false,
                            });
                        }
                    }

                }
                if (model.Price <= model.PriceSale)
                {
                    ViewBag.error = "Giá khuyến mãi phải nhỏ hơn giá sản phẩm";
                }
                else
                {
                    model.CreatedDate = DateTime.Now;
                    model.ModifiedDate = DateTime.Now;
                    model.CreatedBy = (string)Session["FullName"];
                    if (string.IsNullOrEmpty(model.Alias))
                        model.Alias = Filter.FilterChar(model.Title);
                    db.Products.Add(model);
                    db.SaveChanges();
                    TempData["AllertMesssage"] = "Thêm mới thành công";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }
        #endregion

        #region Sửa sản phẩm
        [CustomAuthorize("Admin", "Employee")]
        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            var item = db.Products.Find(id);
            return View(item);
        }

        [CustomAuthorize("Admin", "Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = Filter.FilterChar(model.Title);
                model.ModifiedBy = (string)Session["FullName"];
                db.Products.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["AllertMesssage"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Xóa sản phẩm
        [CustomAuthorize("Admin", "Employee")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion

        #region Sản phẩm đang hoạt động, hiển thị ở home, khuyến mãi
        [CustomAuthorize("Admin", "Employee")]
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }

        [CustomAuthorize("Admin", "Employee")]
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isHome = item.IsHome });
            }
            return Json(new { success = false });
        }

        [CustomAuthorize("Admin", "Employee")]
        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsSale });
            }
            return Json(new { success = false });
        }
        #endregion
    }
}