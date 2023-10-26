using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebCiro.Models;

namespace WebCiro.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly WebCiroDbContext _db;
        public HomeController(WebCiroDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
      


    }
}