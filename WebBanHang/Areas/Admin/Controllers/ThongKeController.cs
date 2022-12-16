using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.Common;
using WebBanHang.Models.EF;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: Admin/ThongKe
        
        [HttpGet]
        public JsonResult MonthlyRevenue()
        {
            ThongKeTruyCap obj = new ThongKeTruyCap();

            List<ReportProduct> objList = obj.monthlyRevenue();
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RevenueByYear()
        {
            ThongKeTruyCap obj = new ThongKeTruyCap();

            List<ReportProduct> objList = obj.revenueByYear();
            return Json(objList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            /*  ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();
              ViewBag.SoLuongNguoiOnline = HttpContext.Application["SoNguoiOnline"].ToString();
              string total = db.orders.Sum(x => x.TotalAmount).ToString();
              CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
              string a = double.Parse(total).ToString("#,###", cul.NumberFormat);
              ViewBag.TotalMoney = a;
              ViewBag.TotalOrder = db.orders.Count().ToString();
              var usersWithRoles = (from user in db.Users
                                    select new
                                    {
                                        UserId = user.Id,
                                        Username = user.UserName,
                                        Email = user.Email,
                                        RoleNames = (from userRole in user.Roles
                                                     join role in db.Roles on userRole.RoleId
                                                     equals role.Id
                                                     select role.Name).ToList()
                                    }).ToList();
              return View();*/
            return View();
        }
      
      
    }
}