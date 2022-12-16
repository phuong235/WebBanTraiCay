using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;

namespace WebBanHang.Controllers
{
    public class OrderController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();


        public ActionResult Index()
        {
            if (Session["idUser"] != null)
            {

                var items = db.Orders.ToList();
                return View(items);
            }
            else
            {
                return View();
            }
        }
        public ActionResult Partial_Paid()
        {
            var items = db.Orders.Where(x => x.TypePayment == 2).ToList();
            items = items.Where(x => x.UserId == (int)Session["idUser"]).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_UnPaid()
        {
            var items = db.Orders.Where(x => x.TypePayment == 1).ToList();
            items = items.Where(x => x.UserId == (int)Session["idUser"]).ToList();
            return PartialView(items);
        }
        public bool UpdateOrderStatus(int id)
        {
            try
            {
                db.Orders.Find(id).TypePayment = 0;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
          
        }
        public ActionResult Partial_Cancelled()
        {
            var items = db.Orders.Where(x => x.TypePayment != 2 && x.TypePayment != 1).ToList();
            items = items.Where(x => x.UserId == (int)Session["idUser"]).ToList();
            return PartialView(items);
        }
        public ActionResult View(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }

        // Xem chi tiết đơn hàng
        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }
    }
}