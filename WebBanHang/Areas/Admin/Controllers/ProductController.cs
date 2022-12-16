
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Data;
using WebBanHang.Models.EF;
using Filter = WebBanHang.Models.Common.Filter;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Emplpoyee")]
    public class ProductController : Controller
    {

        // GET: Admin/Product
        private WebBanHangFinalContext db = new WebBanHangFinalContext();

        public ActionResult Index()
        {
            var items = db.TbProducts;
            ViewBag.NumberOfCategories = db.TbProductCategory.Count();
            ViewBag.TotalProducts = db.TbProductCategory.Count();
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



        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(db.TbProductCategory.ToList(), "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TbProducts model, List<string> Images, List<int> rDefault)
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
                            model.TbProductImage.Add(new TbProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true,
                            });
                        }
                        else
                        {
                            model.TbProductImage.Add(new TbProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false,
                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;

                db.TbProducts.Add(model);
                db.SaveChanges();
                TempData["AllertMesssage"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(db.TbProductCategory.ToList(), "Id", "Title");
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(db.TbProductCategory.ToList(), "Id", "Title");
            var item = db.TbProducts.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TbProducts model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = Filter.FilterChar(model.Title);
                db.TbProducts.Attach(model);
                db.Entry(model).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["AllertMesssage"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.TbProducts.Find(id);
            if (item != null)
            {
                db.TbProducts.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.TbProducts.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.TbProducts.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                db.Entry(item).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isHome = item.IsHome });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = db.TbProducts.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                db.Entry(item).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsSale });
            }
            return Json(new { success = false });
        }


    }
}