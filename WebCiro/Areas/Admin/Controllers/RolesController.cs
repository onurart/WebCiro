using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebCiro.Areas.Admin.Models;
using WebCiro.Models.Authentication;

namespace WebCiro.Areas.Admin.Controllers
{
   
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManeger;
        private readonly RoleManager<AppRole> _rolerManeger;
        public IActionResult Index()
        {
            return View();
        }   public IActionResult RoleCreate()
        {
            return View();
        }
        
        public IActionResult RoleCreate(RoleCreateViewModel requst)
        {
            return View();
        }
    }
}
