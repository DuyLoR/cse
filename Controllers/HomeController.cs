using CSE_website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace CSE_website.Controllers
{
    public class HomeController : Controller
    {
        private readonly CSE_website.Data.CSEWebsiteContext _context;

        public HomeController(CSE_website.Data.CSEWebsiteContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var partners = await _context.Partners.OrderByDescending(p => p.Order).Take(6).ToListAsync();
            ViewBag.Partners = partners;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Search()
        {
            return View();
        }
    }
}