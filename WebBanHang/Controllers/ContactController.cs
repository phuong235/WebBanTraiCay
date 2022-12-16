using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;

namespace WebBanHang.Controllers
{
    public class ContactController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Contact vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(vm.Email);//Email which you are getting 
                                                         //from contact us page 
                    msz.To.Add("vietanh2322001@gmail.com");//Where mail will be sent 
                 
                    msz.Subject = "Lời nhắn phản hồi từ khách hàng: " + vm.Email;
                    msz.Body = "Tên: " + vm.Name + " có lời nhắn: " + vm.Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("vietanh2322001@gmail.com", "oplwimrxxnnqsiuu");

                    smtp.EnableSsl = true;
                    vm.CreatedDate = DateTime.Now;
                    vm.ModifiedDate = DateTime.Now;
                    db.Contacts.Add(vm);
                    db.SaveChanges();
                    smtp.Send(msz);
                    
                    ModelState.Clear();
                    ViewBag.Message = "Cảm ơn bạn đã gửi phản hồi chúng tôi.";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Đã xảy ra lỗi";
                }

            }
            return View();
        }
    }
}