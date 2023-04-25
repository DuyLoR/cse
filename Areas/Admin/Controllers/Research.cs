using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSE_website.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class Research : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddResearch()
        {
            return View();
        }
        public IActionResult EditResearch()
        {
            return View();
        }
    }
}