using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCiro.Core.Entities;
using WebCiro.Repository;

namespace WebCiro.Controllers
{
    
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;
        public DashboardController(AppDbContext db)
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

            // Entity Framework kullanarak veritabanından veri çekme
            var data = await _db.NET_001_CIRO
                .Where(c => c.City == branch)
                .GroupBy(c => c.StockCategories)
                .Select(group => new
                {
                    StockCategories = group.Key,
                    TotalAmount = group.Sum(c => c.Amount),  // Update property name to TotalAmount
                    SpeCode = group.Select(x => x.SpeCode)
                })
                .OrderByDescending(result => result.TotalAmount).Take(12)  // Update property name to TotalAmount
                .ToListAsync();

            // Verileri JSON formatına dönüştürerek döndürme
            return Json(data);
        }
        public IActionResult GetByReportBranch(string branch)
        {
            TempData["branch"] = branch;

            var city = _db.NET_001_CIRO
     .Where(x => x.COUNTRY == "TÜRKİYE")
     .GroupBy(x => x.City)
     .Select(group => group.Key)
     .ToList();
            TempData["cities"] = city;
            return View();
        }
        public async Task<object> GetByReportBranchDataGrid(DataSourceLoadOptions loadOptions)
        {
            var branch = TempData["branch"] as string ?? "DefaultBranch";
            if (string.IsNullOrEmpty(branch))
            {
                return BadRequest("Branch parameter is missing.");
            }

            var data = await _db.NET_001_CIRO
                .Select(item => new NET_001_CIRO
                {
                    Id = item.Id,
                    StockCategories = item.StockCategories,
                    City = item.City,
                    AccountCode = item.AccountCode,
                    Amount = item.Amount ?? 0,
                    Customer = item.Customer,
                    StokName = item.StokName,
                    SpeCode = item.SpeCode,
                    AUXCODE2 = item.AUXCODE2,
                    TlNet = item.TlNet ?? 0,
                    COUNTRY = item.COUNTRY
                })
                .Where(item => item.City == branch)
                .ToListAsync();

            return DataSourceLoader.Load(data, loadOptions);
        }

    }
}
