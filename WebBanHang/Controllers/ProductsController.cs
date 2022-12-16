using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        private WebBanHangEntities db = new WebBanHangEntities();
        public ActionResult Index()
        {
            var items = db.Products.ToList();

            return View(items);

        }
        public ActionResult Detail(int id)
        {
            ViewBag.ListComment = db.Comments.ToList();

            ViewBag.UserId = Session["idUser"];
            ViewBag.Count = db.Comments.Count();
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
            return View(item);
        }
        public ActionResult ProductCategory(int id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.CateId = id;
            return View(items);

        }

        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult AddNewComment()
        {
            return View("Detail");
        }

        [HttpPost]
        public JsonResult AddNewComment(int productid, int userid, string commentmsg, string rate)
        {
           
            try
            {
                var dao = new Comment();
                Comment comment = new Comment();

                comment.Message = commentmsg;
                comment.ProductId = productid;
                comment.UserId = userid;
                comment.Rate = Convert.ToInt16(rate);
                comment.CreatedDate = DateTime.Now;

                bool addcomment = Insert(comment);
                if (addcomment == true)
                {
                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = false
                    });
                }
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        public bool Insert(Comment entity)
        {
            db.Comments.Add(entity);
            db.SaveChanges();
            return true;
        }
    }
}