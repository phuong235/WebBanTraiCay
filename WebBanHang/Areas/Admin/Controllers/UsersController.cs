using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models.Common;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();

        #region Lấy danh sách tài khoản
     /*   [CustomAuthorize("Admin")]*/
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Role);
            ViewBag.AllertMesssage = TempData["AllertMesssage"] as string;
            return View(users.ToList());
        }
        #endregion

        #region Thêm tài khoản
        [CustomAuthorize("Admin")]
        public ActionResult Add()
        {
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Id", "Name");
            return View();
        }

        [CustomAuthorize("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(User user)
        {
                    ViewBag.Role = new SelectList(db.Roles.ToList(), "Id", "Name");
            if (ModelState.IsValid)
            {
                var checkEmail = db.Users.FirstOrDefault(x => x.Email == user.Email);
                var isEmailAlreadyExists = db.Users.Any(x => x.Email == user.Email);
                if (checkEmail == null)
                {
                    user.Password = GetMD5(user.Password);
                    user.CreatedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    user.CreatedBy = (string)Session["FullName"];
                    user.ConfirmPassword = user.Password;
                    db.Users.Add(user);
                    db.SaveChanges();
                    TempData["AllertMesssage"] = "Thêm mới thành công";
                    return RedirectToAction("Index");
                }
                else if(isEmailAlreadyExists)
                {
                    ModelState.AddModelError("Email", "Email bạn đăng ký đã tồn tại");
                    return View(user);
                }
                else
                {
                    ViewBag.error = "Đăng ký thất bại";
                    return View();
                }
            }
          
            return View(user);
        }
        #endregion

        #region Cập nhật tài khoản
        [CustomAuthorize("Admin")]
        public ActionResult Edit(int id)
        {
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Id", "Name");
            var item = db.Users.Find(id);
            return View(item);
        }

        [CustomAuthorize("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Attach(user);
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = (string)Session["FullName"];
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AllertMesssage"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(user);
        }
        #endregion

        #region Xóa tài khoản
        [CustomAuthorize("Admin")]
        public ActionResult Delete(int? id)
        {
            var item = db.Users.Find(id);
            if (item != null)
            {
                db.Users.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion

        #region Đăng nhập
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
                var checkRole = db.Users.Any(x => x.Role.Name == "Customer");
                if (data.Count() > 0)
                {
                
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    Session["Role"] = data.FirstOrDefault().Role.Name;
                    Session["Phone"] = data.FirstOrDefault().Phone;
                    Session["Image"] = data.FirstOrDefault().UserImage;
                    if(checkRole)
                    {
                        ModelState.AddModelError("Email", "Bạn không có quyền truy cập trang web này");
                        return View();
                    }    
                    
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
        #endregion

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        // Mã hóa mật khẩu
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
    }
}
