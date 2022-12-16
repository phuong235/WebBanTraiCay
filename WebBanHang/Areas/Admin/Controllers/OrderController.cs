
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;
using WebBanHang.Models.Common;

namespace WebBanHang.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    public class OrderController : Controller
    {
        
        private WebBanHangEntities db = new WebBanHangEntities();

        // Lấy danh sách đơn hàng
        [CustomAuthorize("Admin", "Employee")]
        public ActionResult Index()
        {
            var items = db.Orders;
            ViewBag.AllertMesssage = TempData["AllertMesssage"] as string;
           
            return View(items);
        }

        #region Xem đơn hàng
        [CustomAuthorize("Admin", "Employee")]
        public ActionResult View(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }

        // Xem chi tiết đơn hàng
        [CustomAuthorize("Admin", "Employee")]
        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x=> x.OrderId == id).ToList();
            return PartialView(items);
        }
        #endregion

        #region Cập nhật trạng thái đơn hàng
        [CustomAuthorize("Admin", "Employee")]
        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                item.TypePayment = trangthai;
                item.ModifiedBy = (string)Session["FullName"];
                db.Entry(item).Property(x => x.TypePayment).IsModified = true;
                db.SaveChanges();
                TempData["AllertMesssage"] = "Cập nhật trạng thái thành công";
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "UnSuccess", Success = false });
        }
        #endregion
    }
}