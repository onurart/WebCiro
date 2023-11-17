using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebCiro.Models;
namespace WebCiro.Controllers
{
    public class DashboardController : Controller
    {
        private readonly WebCiroDbContext _db;
        public DashboardController(WebCiroDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetByReportBranchPie()
        {
            var branch = TempData["branch"] as string ?? "DefaultBranch";

            var data = await _db.WI_Ste_Prd_Ctr_Day
                .Where(c => c.CITY == branch)
                .GroupBy(c => c.PRODUCT_CATEGORY)
                .Select(group => new
                {
                    ProductCategory = group.Key,
                    TotalAmount = group.Sum(c => c.INVOICE_COUNT),  
                    SpeCode = group.Select(x => x.PRODUCT_CATEGORY)
                })
                .OrderByDescending(result => result.TotalAmount).Take(12) 
                .ToListAsync();

            return Json(data);
        }

        public IActionResult GetByReportBranch(string branch)
        {
            TempData["branch"] = branch;
            var city = _db.WI_DashboardTbl
           .Where(x => x.City == branch)
           .GroupBy(x => x.City)
           .Select(group => group.Key)
           .ToList();
            TempData["cities"] = city;
            return View();
        }
        public async Task<object> GetByReportBranchDataGrid(DataSourceLoadOptions loadOptions)
        {
            var branch = TempData["branch"] as string ?? "DefaultBranch";
            TempData.Keep("branch");

            if (string.IsNullOrEmpty(branch))
            {
                return BadRequest("Branch parameter is missing.");
            }

            var data = await _db.WI_State_Sales
                .Where(item => item.CITY == branch)
                .Select(item => new WI_State_Sales
                {
                    Id = item.Id,
                    INVOICE_NUMBER = item.INVOICE_NUMBER,
                    INVOICE_DATE = item.INVOICE_DATE,
                    // INVOICE_YEAR = item.INVOICE_YEAR,
                    INVOICE_MONTH = item.INVOICE_MONTH,
                    // INVOICE_WEEK = item.INVOICE_WEEK,
                    CITY = item.CITY,
                    CUSTOMER_CODE = item.CUSTOMER_CODE,
                    CUSTOMER_NAME = item.CUSTOMER_NAME.Substring(0, 20),
                    PRODUCT_CATEGORY = item.PRODUCT_CATEGORY,
                    PRODUCT_BRAND = item.PRODUCT_BRAND,
                    PRODUCT_CODE = item.PRODUCT_CODE,
                    PRODUCT_NAME = item.PRODUCT_NAME.Substring(0, 20),
                    PRODUCT_QUANTITY = item.PRODUCT_QUANTITY,
                    NET_AMOUNT = Convert.ToDouble(item.NET_AMOUNT),
                    PRODUCT_SUPPLIER = item.PRODUCT_SUPPLIER.Substring(0, 20),
                })
                .ToListAsync();

            return DataSourceLoader.Load(data, loadOptions);
        }
        public async Task<object> GetByReportBranchDataMonthGrid(DataSourceLoadOptions loadOptions)
        {
            var branch = TempData["branch"] as string ?? "DefaultBranch";
            TempData.Keep("branch"); // TempData'deki "branch" verisini bir sonraki isteğe kadar koru

            if (string.IsNullOrEmpty(branch))
            {
                return BadRequest("Branch parameter is missing.");
            }

            var currentMonth = DateTime.Now.Month;

            var data = await _db.WI_State_Sales
                .Where(item => item.INVOICE_MONTH == currentMonth && item.CITY == branch)
                .Select(item => new WI_State_Sales
                {
                    Id = item.Id,
                    INVOICE_NUMBER = item.INVOICE_NUMBER,
                    INVOICE_DATE = item.INVOICE_DATE,
                    // INVOICE_YEAR = item.INVOICE_YEAR,
                    INVOICE_MONTH = item.INVOICE_MONTH,
                    INVOICE_WEEK = item.INVOICE_WEEK,
                    CITY = item.CITY,
                    CUSTOMER_CODE = item.CUSTOMER_CODE,
                    CUSTOMER_NAME = item.CUSTOMER_NAME.Length > 20 ? item.CUSTOMER_NAME.Substring(0, 20) : item.CUSTOMER_NAME,
                    PRODUCT_CATEGORY = item.PRODUCT_CATEGORY,
                    PRODUCT_BRAND = item.PRODUCT_BRAND,
                    PRODUCT_CODE = item.PRODUCT_CODE,
                    PRODUCT_NAME = item.PRODUCT_NAME.Length > 20 ? item.PRODUCT_NAME.Substring(0, 20) : item.PRODUCT_NAME,
                    PRODUCT_QUANTITY = item.PRODUCT_QUANTITY,
                    // NET_AMOUNT = (double)item.NET_AMOUNT,
                    PRODUCT_SUPPLIER = item.PRODUCT_SUPPLIER.Length > 20 ? item.PRODUCT_SUPPLIER.Substring(0, 20) : item.PRODUCT_SUPPLIER,
                })
                .ToListAsync();

            return DataSourceLoader.Load(data, loadOptions);
        }


        public async Task<object> GetByReportBranchDataWeekGrid(DataSourceLoadOptions loadOptions)
        {
            var branch = TempData["branch"] as string ?? "DefaultBranch";
            TempData.Keep("branch"); // TempData'deki "branch" verisini bir sonraki isteğe kadar koru

            if (string.IsNullOrEmpty(branch))
            {
                return BadRequest("Branch parameter is missing.");
            }



            int currentWeekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                DateTime.Now,
                CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);

            var data = await _db.WI_State_Sales
                .Where(item => item.CITY == branch && item.INVOICE_WEEK == currentWeekNumber)
                .Select(item => new WI_State_Sales
                {
                    Id = item.Id,
                    INVOICE_NUMBER = item.INVOICE_NUMBER,
                    INVOICE_DATE = item.INVOICE_DATE,
                    // INVOICE_YEAR = item.INVOICE_YEAR,
                    INVOICE_MONTH = item.INVOICE_MONTH,
                    INVOICE_WEEK = item.INVOICE_WEEK,
                    CITY = item.CITY,
                    CUSTOMER_CODE = item.CUSTOMER_CODE,
                    CUSTOMER_NAME = item.CUSTOMER_NAME.Length > 20 ? item.CUSTOMER_NAME.Substring(0, 20) : item.CUSTOMER_NAME,
                    PRODUCT_CATEGORY = item.PRODUCT_CATEGORY,
                    PRODUCT_BRAND = item.PRODUCT_BRAND,
                    PRODUCT_CODE = item.PRODUCT_CODE,
                    PRODUCT_NAME = item.PRODUCT_NAME.Length > 20 ? item.PRODUCT_NAME.Substring(0, 20) : item.PRODUCT_NAME,
                    PRODUCT_QUANTITY = item.PRODUCT_QUANTITY,
                    // NET_AMOUNT = (double)item.NET_AMOUNT,
                    PRODUCT_SUPPLIER = item.PRODUCT_SUPPLIER.Length > 20 ? item.PRODUCT_SUPPLIER.Substring(0, 20) : item.PRODUCT_SUPPLIER,
                })
                .ToListAsync();

            return DataSourceLoader.Load(data, loadOptions);
        }













        public async Task<IActionResult> OrdersPendingApproval()
        {
            return PartialView("_OrdersPendingApproval");
        }
        public IActionResult GetOrdersPendingApproval()
        { return View(); }
        public IActionResult ThoseWaitingforShipment()
        { return View(); }
        [HttpGet]
        public async Task<object> GetOrdersPendingApprovalGrid(DataSourceLoadOptions loadOptions)
        {
            var data = await _db.WI_ReportApprove
                .Where(item => item.Status == "Onay Bekliyor")
                .Select(item => new WI_ReportApprove
                {
                    Id = item.Id,
                    SalesmanName = item.SalesmanName,
                    CustomerName = item.CustomerName.Substring(0, 25),
                    CustomerCode = item.CustomerCode,
                    OrderDate = item.OrderDate,
                    OrderNumber = item.OrderNumber,
                    City = item.City,
                    ProductName = item.ProductName.Substring(0, 25),
                    TotalTl = item.TotalTl,
                    VehicleType = Convert.ToString(item.VehicleType),
                    VehicleBrand = item.VehicleBrand,
                    PartBrand = Convert.ToString(item.PartBrand),
                    Category = Convert.ToString(item.Category),
                    Status = item.Status,


                })
                .ToListAsync();

            return DataSourceLoader.Load(data, loadOptions);
        }
        public async Task<object> ThoseWaitingforShipmentlGrid(DataSourceLoadOptions loadOptions)
        {
            var data = await _db.WI_ReportApprove
                .Where(item => item.Status == "Sevk Bekliyor")
                .Select(item => new WI_ReportApprove
                {
                    Id = item.Id,
                    SalesmanName = item.SalesmanName,
                    CustomerName = item.CustomerName.Substring(0, 25),
                    CustomerCode = item.CustomerCode,
                    OrderDate = item.OrderDate,
                    OrderNumber = item.OrderNumber,
                    City = item.City,
                    ProductName = item.ProductName.Substring(0, 25),
                    TotalTl = item.TotalTl,
                    VehicleType = Convert.ToString(item.VehicleType),
                    VehicleBrand = item.VehicleBrand,
                    PartBrand = Convert.ToString(item.PartBrand),
                    Category = Convert.ToString(item.Category),
                    Status = item.Status,
                })
                .ToListAsync();

            return DataSourceLoader.Load(data, loadOptions);
        }

    }
}
