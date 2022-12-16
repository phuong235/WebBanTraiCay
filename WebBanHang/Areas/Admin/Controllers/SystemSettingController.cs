using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models.Common;

namespace WebBanHang.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    public class SystemSettingController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();
        // GET: Admin/SystemSetting

        [CustomAuthorize("Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize("Admin")]
        public ActionResult Partial_Setting()
        {
            return PartialView();
        }

        #region Thêm cấu hình
        [CustomAuthorize("Admin")]
        [HttpPost]
        public ActionResult AddSetting(SettingSystemViewModel req)
        {
            SystemSetting set = null;
            var checkTitle = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitle"));
            if (checkTitle == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingTitle";
                set.SettingValue = req.SettingTitle;
                set.SettingDescription = req.SettingTitle;
                db.SystemSettings.Add(set);

            }
            else
            {
                checkTitle.SettingValue = req.SettingTitle;
                checkTitle.SettingDescription = req.SettingTitle;
                db.Entry(checkTitle).State = System.Data.Entity.EntityState.Modified;
            }
            var checkLogo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingLogo"));
            if (checkLogo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingLogo";
                set.SettingValue = req.SettingLogo;
                set.SettingDescription = req.SettingLogo;
                db.SystemSettings.Add(set);

            }
            else
            {
                checkLogo.SettingValue = req.SettingLogo;
                checkLogo.SettingDescription = req.SettingLogo;
                db.Entry(checkLogo).State = System.Data.Entity.EntityState.Modified;
            }
            var email = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingEmail"));
            if (email == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingEmail";
                set.SettingValue = req.SettingEmail;
                set.SettingDescription = req.SettingEmail;
                db.SystemSettings.Add(set);

            }
            else
            {
                email.SettingValue = req.SettingEmail;
                email.SettingDescription = req.SettingEmail;
                db.Entry(email).State = System.Data.Entity.EntityState.Modified;
            }

            var hotline = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingHotline"));
            if (hotline == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingHotline";
                set.SettingValue = req.SettingHotLine;
                set.SettingDescription = req.SettingHotLine;
                db.SystemSettings.Add(set);

            }
            else
            {
                hotline.SettingValue = req.SettingHotLine;
                hotline.SettingDescription = req.SettingHotLine;
                db.Entry(hotline).State = System.Data.Entity.EntityState.Modified;
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View("Index");
        }
        #endregion
    }
}