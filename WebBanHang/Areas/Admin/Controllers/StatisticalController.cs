using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;
using WebBanHang.Models.Common;

namespace WebBanHang.Areas.Admin.Controllers
{

    public class StatisticalController : Controller
    {
        // GET: Admin/Statistical
        private WebBanHangEntities db = new WebBanHangEntities();

     
        public ActionResult Index()
        {
            return View();
        }

   
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

      
        public JsonResult RevenueByQuarter()
        {
            ThongKeTruyCap obj = new ThongKeTruyCap();

            List<ReportProduct> objList = obj.revenueByQuarter();
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TopSelling()
        {
            ThongKeTruyCap obj = new ThongKeTruyCap();

            List<ReportProduct> objList = obj.TopBanChay();
            return Json(objList, JsonRequestBehavior.AllowGet);
        }


        public void ExportRevenueByMonth()
        {
            var items = db.Orders.ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "WebSite";
            ws.Cells["B1"].Value = "Fresh Fruit";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Thống kê doanh thu theo năm";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Tháng";
            ws.Cells["B6"].Value = "Năm";
            ws.Cells["C6"].Value = "Doanh thu";
    
            ws.Cells["A6:C6"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            ws.Cells["A6:C6"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("yellow")));

            int rowStart = 7;

            var revenueByMonth = db.Orders.Where(x => x.TypePayment == 2)
                                            .GroupBy(x => new { x.CreatedDate.Month, x.CreatedDate.Year })
                                            .Select(g => new { Month = g.Key.Month, Year = g.Key.Year, total = g.Sum(x => x.TotalAmount) })
                                            .ToList();
         /*var sum = db.Orders.GroupBy(x => x.CreatedDate.Year).Select(g => new { Year = g.Key, total = g.Sum(x => x.TotalAmount) }).ToList();*/
            foreach (var i in revenueByMonth)
            {
                ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                ws.Cells[string.Format("A{0}", rowStart)].Value = i.Month;
                ws.Cells[string.Format("B{0}", rowStart)].Value = i.Year;
                ws.Cells[string.Format("C{0}", rowStart)].Value = i.total + " VND";
                rowStart++;

            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }
    }
}