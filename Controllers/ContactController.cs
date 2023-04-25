using Microsoft.AspNetCore.Mvc;

namespace CSE_website.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
