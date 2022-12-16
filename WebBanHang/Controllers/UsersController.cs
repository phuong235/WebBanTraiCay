using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class UsersController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();
        // GET: Users
        public ActionResult Index()
        {
            if (Session["idUser"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        public ActionResult Profile(int id)
        {
            var item = db.Users.Find(id);
            var data = db.Users.Where(s => s.Id.Equals(id));
      
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(User model)
        {
            if (ModelState.IsValid)
            {
                db.Users.Attach(model);
                model.ModifiedDate = DateTime.Now;
                db.Entry(model).State = EntityState.Modified;

                db.SaveChanges();
                TempData["AllertMesssage"] = "Thông tin đã được cập nhật";                                     
                ViewBag.AllertMesssage = TempData["AllertMesssage"] as string;
                return View(model);
            }
            return View(model);
        }


        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.CreatedDate = DateTime.Now;
                    _user.ModifiedDate = DateTime.Now;
                    _user.Password = GetMD5(_user.Password);
                    _user.ConfirmPassword = _user.Password;
                    _user.RoleId = 2;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Users.Add(_user);
                    db.SaveChanges();
                    TempData["AllertMesssage"] = "Đăng ký thành công";
                    ViewBag.AllertMesssage = TempData["AllertMesssage"] as string;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";
                    return View();
                }


            }
            return View();


        }
       
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    Session["Phone"] = data.FirstOrDefault().Phone;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Tài khoản hoặc mật khẩu không đúng";
                    return View();
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }


        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Users/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("vietanh2322001@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "oplwimrxxnnqsiuu"; // Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>Chúng tôi đã nhận được yêu cầu đặt lại mật khẩu tài khoản của bạn. Vui lòng nhấp vào liên kết bên dưới để đặt lại mật khẩu của bạn" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }


        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            //Verify Email ID
            //Generate Reset password link 
            //Send Email 
            string message = "";
        


            var account = db.Users.Where(a => a.Email == Email).FirstOrDefault();
            if (account != null)
            {
                //Send email for reset password
                string resetCode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(account.Email, resetCode, "ResetPassword");
                account.ResetPasswordCode = resetCode;
                //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                //in our model class in part 1
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                message = "Liên kết đặt lại mật khẩu đã được gửi đến email của bạn. Vui lòng kiểm tra hòm thư trong email của bạn để đặt lại mật khẩu. ";
            }
            else
            {
                message = "Không tìm thấy tài khoản";
            }

            ViewBag.Message = message;
            return View();
        }
        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (WebBanHangEntities dc = new WebBanHangEntities())
            {
                var user = dc.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                if (user != null)
                {
                    user.Password = GetMD5(model.NewPassword);
                    user.ConfirmPassword = user.Password;
                    user.ResetPasswordCode = "";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "Mật khẩu mới đã được cập nhật. Vui lòng quay lại trang đăng nhập để đăng nhập";
                }

            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }
    }

}