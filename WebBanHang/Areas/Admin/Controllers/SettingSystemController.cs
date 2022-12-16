using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Data;
using WebBanHang.Models.EF;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class SettingSystemController : Controller
    {
        private WebBanHangFinalContext db = new WebBanHangFinalContext();
        // GET: Admin/SeetingSystem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_Setting()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddSetting(SettingSystemViewModel req)
        {
            TbSystemSetting set = null;
            var checkTitle = db.TbSystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingTitle"));
            if (checkTitle == null)
            {
                set = new TbSystemSetting();
                set.SettingKey = "SettingTitle";
                set.SettingValue = req.SettingTitle;
                db.TbSystemSetting.Add(set);
            }
            else
            {
                checkTitle.SettingValue = req.SettingTitle;
                db.Entry(checkTitle).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
            }
            var checkLogo = db.TbSystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingLogo"));
            if (checkLogo == null)
            {
                set = new TbSystemSetting();
                set.SettingKey = "SettingLogo";
                set.SettingValue = req.SettingLogo;
                db.TbSystemSetting.Add(set);

            }
            else
            {
                checkLogo.SettingValue = req.SettingLogo;
                db.Entry(checkLogo).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
            }
            var email = db.TbSystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingEmail"));
            if (email == null)
            {
                set = new TbSystemSetting();
                set.SettingKey = "SettingEmail";
                set.SettingValue = req.SettingEmail;
                db.TbSystemSetting.Add(set);

            }
            else
            {
                email.SettingValue = req.SettingEmail;
                db.Entry(email).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
            }

            var hotline = db.TbSystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingHotline"));
            if (hotline == null)
            {
                set = new TbSystemSetting();
                set.SettingKey = "SettingHotline";
                set.SettingValue = req.SettingHotLine;
                db.TbSystemSetting.Add(set);

            }
            else
            {
                hotline.SettingValue = req.SettingHotLine;
                db.Entry(hotline).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
            }

            var titleSeo = db.TbSystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingTitleSeo"));
            if (titleSeo == null)
            {
                set = new TbSystemSetting();
                set.SettingKey = "SettingTitleSeo";
                set.SettingValue = req.SettingTitleSeo;
                db.TbSystemSetting.Add(set);

            }
            else
            {
                titleSeo.SettingValue = req.SettingTitleSeo;
                db.Entry(titleSeo).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
            }

            var desSeo = db.TbSystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingDesSeo"));
            if (desSeo == null)
            {
                set = new TbSystemSetting();
                set.SettingKey = "SettingDesSeo";
                set.SettingValue = req.SettingDesSeo;
                db.TbSystemSetting.Add(set);

            }
            else
            {
                desSeo.SettingValue = req.SettingDesSeo;
                db.Entry(desSeo).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
            }

            var keySeo = db.TbSystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingKeySeo"));
            if (keySeo == null)
            {
                set = new TbSystemSetting();
                set.SettingKey = "SettingKeySeo";
                set.SettingValue = req.SettingKeySeo;
                db.TbSystemSetting.Add(set);

            }
            else
            {
                keySeo.SettingValue = req.SettingKeySeo;
                db.Entry(keySeo).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
            return View("Index");
        }
    }
}