﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Partial_Subscribe()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscribe(Subscribe req)
        {
            if (ModelState.IsValid)
            {
                db.Subscribes.Add(new Subscribe { Email = req.Email, CreatedDate = DateTime.Now });
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return View("Partial_Subscribe");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Refresh()
        {
            var item = new StatisticModel();

            ViewBag.Visitors_online = HttpContext.Application["visitors_online"];
            item.HomNay = HttpContext.Application["HomNay"].ToString();
            item.HomQua = HttpContext.Application["HomQua"].ToString();
            item.TuanNay = HttpContext.Application["TuanNay"].ToString();
            item.TuanTruoc = HttpContext.Application["TuanTruoc"].ToString();
            item.ThangNay = HttpContext.Application["ThangNay"].ToString();
            item.ThangTruoc = HttpContext.Application["ThangTruoc"].ToString();
            item.TatCa = HttpContext.Application["TatCa"].ToString();

            return PartialView(item);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}