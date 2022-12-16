using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;
using WebBanHang.Models.Common;


namespace WebBanHang.Controllers
{
    public class ShoppingCartController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult CheckOut()
        {

            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult CheckOutSuccess()
        {


            return View();
        }
        public ActionResult Partial_Item_ThanhToan()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.items);
            }
            return PartialView();
        }
        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.items);
            }
            return PartialView();
        }

        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Count = cart.items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }
        [HttpPost]
        public bool CheckOut(string data, string ward, string district, string city)
        {
            try
            {
                OrderViewModel req = JsonConvert.DeserializeObject<OrderViewModel>(data);
                var code = new { Success = false, Code = -1 };
                if (ModelState.IsValid)
                {
                    ShoppingCart cart = (ShoppingCart)Session["Cart"];
                    if (cart != null)
                    {
                        Order order = new Order();
                        order.UserId = (int?)Session["idUser"];
                        order.CustomerName = req.CustomerName;
                        order.Phone = req.Phone;
                        order.Email = req.Email;
                        order.Street = req.Street;
                        order.Ward = ward;
                        order.District = district;
                        order.City = city;
                        foreach (var item in cart.items)
                        {
                            var detail = new OrderDetail();
                            detail.ProductId = item.ProductId;
                            detail.Quantity = item.Quantity;
                            detail.Price = item.Price;
                            order.OrderDetails.Add(detail);
                            SellProduct(item.ProductId, item.Quantity);
                        }
                        cart.items.ForEach(x => order.OrderDetails.Add(new OrderDetail
                        {
                            ProductId = x.ProductId,
                            Quantity = x.Quantity,
                            Price = x.Price,
                        }));
                        order.TotalAmount = cart.items.Sum(x => (x.Price * x.Quantity));
                        order.TypePayment = req.TypePayment;
                        order.CreatedDate = DateTime.Now;
                        order.ModifiedDate = DateTime.Now;
                        order.CreatedBy = req.CustomerName;
                        Random rd = new Random();
                        order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                        db.Orders.Add(order);
                        db.SaveChanges();
                        // send mail cho khách hàng
                        var strSanPham = "";
                        var thanhTien = decimal.Zero;
                        var tongTien = decimal.Zero;
                        foreach (var sp in cart.items)
                        {
                            strSanPham += "<tr>";
                            strSanPham += "<td>" + sp.ProductName + "</td>";
                            strSanPham += "<td>" + sp.Quantity + "</td>";
                            strSanPham += "<td>" + Common.Common.FormatNumber(sp.TotalPrice, 0) + "</td>";
                            strSanPham += "</tr>";
                            thanhTien += sp.Price * sp.Quantity;
                        }
                        tongTien = thanhTien;
                        string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html"));
                        contentCustomer = contentCustomer.Replace("{{MaDon}}", order.Code);
                        contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
                        contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                        contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", order.CustomerName);
                        contentCustomer = contentCustomer.Replace("{{Phone}}", order.Phone);
                        contentCustomer = contentCustomer.Replace("{{Email}}", order.Email);
                        contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", order.Street + ", " + order.Ward + ", " + order.District + ", " + order.City);
                        contentCustomer = contentCustomer.Replace("{{ThanhTien}}", Common.Common.FormatNumber(thanhTien, 0));
                        contentCustomer = contentCustomer.Replace("{{TongTien}}", Common.Common.FormatNumber(tongTien, 0));
                        Common.Common.SendMail("ShopOnline", "Đơn hàng #" + order.Code, contentCustomer.ToString(), req.Email);
                        cart.ClearCart();
                        return true;
                    }
                }


            }
            catch (Exception ex)
            {
                return false;
            }
            return false;

        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var db = new WebBanHangEntities();
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);

           

            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
              /*  if (checkProduct.Quantity < cart.)
                {
                    checkProduct.Quantity--;
                    code = new { Success = false, msg = "het hang", code = -1, Count = 0 };
                }*/
                if (cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategory.Title,
                    Alias = checkProduct.Alias,
                    Quantity = quantity

                };
                if (checkProduct.ProductImages.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImages.FirstOrDefault(x => x.IsDefault).Image;
                }
                item.Price = checkProduct.Price;
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = (int)checkProduct.PriceSale;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Sản phẩm đã được thêm vào giỏ hàng", code = -1, Count = cart.items.Count };


            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);
                if (quantity > checkProduct.Quantity)
                {
                    return Json(new { Success = false });
                }
                cart.UpdateQuantity(id, quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.items.FirstOrDefault(x => x.ProductId == id);
                if (checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, Count = cart.items.Count };
                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.ClearCart();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        public bool SellProduct(int productId, int quantity)
        {
            var product = db.Products.Find(productId);
            if (product.Quantity < quantity)
            {
                return false;
            }
            product.Quantity -= quantity;
            return true;
        }
    }
}