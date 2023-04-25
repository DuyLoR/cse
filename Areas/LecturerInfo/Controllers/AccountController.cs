using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CSE_website.Areas.LecturerInfo.Controllers
{
    [Authorize(Roles = "GiangVien")]
    [Area("LecturerInfo")]
    public class AccountController : Controller
    {
        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}
