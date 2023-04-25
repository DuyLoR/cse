using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CSE_website.Areas.LecturerInfo.Controllers
{
    [Authorize(Roles = "GiangVien")]
    [Area("LecturerInfo")]
    public class LecturerController : Controller
    {
        private readonly CSE_website.Data.CSEWebsiteContext _content;
        public LecturerController(CSE_website.Data.CSEWebsiteContext content)
        {
            _content = content;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateInfo()
        {
            return View();
        }
    }
}
