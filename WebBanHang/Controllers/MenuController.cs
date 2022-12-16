using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;


namespace WebBanHang.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        private WebBanHangEntities db = new WebBanHangEntities();
        public ActionResult MenuTop()
        {
            var items = db.Menus.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop", items);
        }
        public ActionResult MenuProductCategory()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", items);
        }

        public ActionResult MenuLeft(int? id)
        {
            if(id != null)
            {
                ViewBag.CateId = id;
            }

            var items = db.ProductCategories.ToList();
            return PartialView("_MenuLeft", items);
        }

        public ActionResult MenuArrivals()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("MenuArrivals", items);
        }
    }
}