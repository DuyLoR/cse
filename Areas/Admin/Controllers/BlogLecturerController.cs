using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CSE_website.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BlogLecturerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddBlogLecturer()
        {
            return View();
        }
        public IActionResult EditBlogLecturer()
        {
            return View();
        }
    }
}
