using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        private WebBanHangEntities db = new WebBanHangEntities();
        public ActionResult Index()
        {
            var items = db.News.ToList();
            return View(items);
        }
        public ActionResult Partial_News_Home()
        {
            var items = db.News.Take(3).ToList();
            return PartialView(items);


        }
        public ActionResult Detail(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                db.News.Attach(item);
                db.SaveChanges();
            }
            return View(item);
        }

    }
}