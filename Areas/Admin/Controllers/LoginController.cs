using CSE_website.Data;
using CSE_website.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.EntityFrameworkCore;

namespace CSE_website.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly CSE_website.Data.CSEWebsiteContext _context;

        public LoginController(CSEWebsiteContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin", controller = "Home", action = "Index" });
                }
                if (User.IsInRole("GiangVien"))
                {
                    return RedirectToAction("Index", "Lecturer", new { area = "LecturerInfo", controller = "Lecturer", action = "UpdateInfo" });
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            if (ModelState.IsValid)
            {
                // kiểm tra mật khẩu
                if (model.Password.Length < 8)
                {
                    ModelState.AddModelError("Password", "Mật khẩu không hợp lệ.");
                    return View(model);
                }

                var account = _context.Accounts.Include(a => a.Permission).SingleOrDefault(a => a.Email == model.Email);
                if (account == null || !BCrypt.Net.BCrypt.Verify(model.Password, account.Password) || !account.Status || account.Permission == null)
                {
                    ModelState.AddModelError("Email", "Email hoặc mật khẩu không chính xác!");
                    return View(model);
                }
                // save cookie
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, account.Email),
                    new Claim(ClaimTypes.Role, account.Permission.Name.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    authProperties
                    );
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult ResetPassWord()
        {
            return View();
        }
    }
}
