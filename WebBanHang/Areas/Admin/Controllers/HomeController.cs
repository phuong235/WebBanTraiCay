using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models.Common;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {

        // Trang chủ Admin
        [CustomAuthorize("Admin", "Employee")]
        public ActionResult Index()
        {
            return View();
        }

        // Trang báo lỗi phân quyền
        public ActionResult Error()
        {
            return View();
        }
    }
}