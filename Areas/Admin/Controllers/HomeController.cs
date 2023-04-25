using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CSE_website.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly CSE_website.Data.CSEWebsiteContext _context;

        public HomeController(CSE_website.Data.CSEWebsiteContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}