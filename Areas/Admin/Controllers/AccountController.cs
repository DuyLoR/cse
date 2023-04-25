using CSE_website.Data;
using CSE_website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

namespace CSE_website.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly CSEWebsiteContext _context;

        public AccountController(CSEWebsiteContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                List<Account> accounts = await _context.Accounts.Include(a => a.Permission).ToListAsync();
                ViewBag.Accounts = accounts;
                if (TempData["AlertMessage"] != null)
                {
                    ViewBag.SuccessMessage = TempData["AlertMessage"];
                }
                if (TempData["AlertMessageError"] != null)
                {
                    ViewBag.ErrorMessage = TempData["AlertMessageError"];
                }
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
        public async Task<IActionResult> AddAccount()
        {
            if (_context.Accounts.Include(a => a.Permission).Any(a => a.Permission.Name == "Admin"))
            {
                ViewBag.Permissions = await _context.Permissions.Where(p => p.Name != "Admin").ToListAsync();
                ViewBag.PermissionSelected = await _context.Permissions.FirstOrDefaultAsync(p => p.Name == "GiangVien");
            }
            else
            {
                ViewBag.Permissions = await _context.Permissions.ToListAsync();
                ViewBag.PermissionSelected = "";
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount(AccountViewModel accountvm)
        {
            // Xóa tất cả các lỗi trong ModelState
            ModelState.Clear();

            // Kiểm tra các điều kiện hợp lệ và thêm các lỗi tùy chỉnh
            if (string.IsNullOrWhiteSpace(accountvm.Email))
            {
                ModelState.AddModelError("Email", "Email không được để trống!");
            }
            else if (!Regex.IsMatch(accountvm.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ModelState.AddModelError("Email", "Định dạng email không hợp lệ!");
            }

            if (string.IsNullOrWhiteSpace(accountvm.Password))
            {
                ModelState.AddModelError("Password", "Mật khẩu không được để trống!");
            }


            if (ModelState.IsValid)
            {
                // Kiểm tra email đã được sử dụng hay chưa
                var existingAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == accountvm.Email);
                if (existingAccount != null)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng.");
                    return View(accountvm);
                }
                //kiểm tra quyền admin
                Permission permission = await _context.Permissions.FirstOrDefaultAsync(a => a.Id == accountvm.PermissionId);
                if (permission.Name == "Admin" && _context.Accounts.Include(a => a.Permission).Any(a => a.Permission.Name == "Admin"))
                {
                    ModelState.AddModelError("Email", "Quyền email không hợp lệ.");
                }
                // Lưu account vào database
                accountvm.Password = BCrypt.Net.BCrypt.HashPassword(accountvm.Password);
                Account account = new Account
                {
                    Id = accountvm.Id,
                    Email = accountvm.Email,
                    Permission = permission,
                    Status = accountvm.Status,
                    Password = accountvm.Password,
                };
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Thêm tài khoản thành công!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Permissions = await _context.Permissions.ToListAsync();
            return View(accountvm);
        }

        [HttpGet]
        public async Task<IActionResult> EditAccount(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return BadRequest();
                }
                //nếu có admin và account hiện tại không phải admin
                var hasAdmin = await _context.Accounts.Include(a => a.Permission).AnyAsync(a => a.Permission.Name == "Admin");
                var targetAccount = await _context.Accounts.Include(a => a.Permission).FirstOrDefaultAsync(a => a.Id == id && a.Permission.Name != "Admin");
                if (hasAdmin && targetAccount != null)
                {
                    ViewBag.Permissions = await _context.Permissions.Where(p => p.Name != "Admin").ToListAsync();
                }
                else
                {
                    ViewBag.Permissions = await _context.Permissions.ToListAsync();
                }
                Account account = await _context.Accounts.Include(a => a.Permission).FirstOrDefaultAsync(a => a.Id == id);
                ViewBag.PermissionSelected = await _context.Permissions.FirstOrDefaultAsync(p => p.Id == account.Permission.Id);
                AccountViewModel accountvm = new AccountViewModel
                {
                    Id = account.Id,
                    Email = account.Email,
                    Permission = account.Permission,
                    PermissionId = account.Permission?.Id,
                    Status = account.Status,
                };
                return View(accountvm);
            }

            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(int id, AccountViewModel accountvm)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                //kiểm tra xem id truyền vào lúc đầu có giống lúc gửi không
                if (id != accountvm.Id)
                {
                    return BadRequest("Id không hợp lệ");
                }
                //kiểm tra account có tồn tại không
                Account account = await _context.Accounts.Include(a => a.Permission).FirstOrDefaultAsync(a => a.Id == accountvm.Id);
                if (account == null)
                {
                    return NotFound();
                }
                //validate đổi mật khẩu
                if (accountvm.ChangePassword)
                {
                    if (accountvm.Password != accountvm.ConfirmPassword)
                    {
                        ModelState.AddModelError("Password", "Mật khẩu mới không khớp.");
                        ModelState.AddModelError("ConfirmPassword", "Mật khẩu mới không khớp.");
                        return View(accountvm);
                    }
                    else
                    {
                        var hashPassword = BCrypt.Net.BCrypt.HashPassword(accountvm.Password);
                        account.Password = hashPassword;
                    }
                }
                //mếu account là admin thì không cho đổi quyền
                var isAdmin = await _context.Accounts.Include(a => a.Permission).AnyAsync(a => a.Id == id && a.Permission.Name == "Admin");
                var isPermissionAdmin = await _context.Permissions.AnyAsync(p => p.Id == accountvm.PermissionId && p.Name == "Admin");
                if (!isAdmin)
                {
                    //nếu không chọn quyền admin
                    if (!isPermissionAdmin)
                    {
                        var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.Id == accountvm.PermissionId);
                        account.Permission = permission;
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Quyền email không hợp lệ.");
                        ViewBag.PermissionSelected = await _context.Permissions.FirstOrDefaultAsync(p => p.Id == account.Permission.Id);
                        ViewBag.Permissions = await _context.Permissions.Where(p => p.Name != "Admin").ToListAsync();
                        return View(accountvm);
                    }
                }
                else if (!isPermissionAdmin)
                {
                    ModelState.AddModelError("Email", "Quyền email không hợp lệ.");
                    ViewBag.PermissionSelected = await _context.Permissions.FirstOrDefaultAsync(p => p.Id == account.Permission.Id);
                    ViewBag.Permissions = await _context.Permissions.ToListAsync();
                    return View(accountvm);
                }
                account.Status = accountvm.Status;

                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Sửa thông tin tài khoản thành công!";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Email đã được sử dụng.");
            ViewBag.Permissions = await _context.Permissions.ToListAsync();
            return View(accountvm);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            Account account = await _context.Accounts.Include(a => a.Permission).FirstOrDefaultAsync(a => a.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            if (account.Permission.Name != "Admin")
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Xóa tài khoản thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["AlertMessageError"] = "Xóa tài khoản không thành công!";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return BadRequest();
                }

                Account account = await _context.Accounts.Include(a => a.Permission).FirstOrDefaultAsync(a => a.Id == id);
                AccountViewModel accountvm = new AccountViewModel
                {
                    Id = account.Id,
                    Email = account.Email,
                    Permission = account.Permission,
                    PermissionId = account.Permission?.Id,
                    Status = account.Status,
                };
                return View(accountvm);
            }

            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(int id, AccountViewModel accountvm)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                if (id != accountvm.Id)
                {
                    return BadRequest("Id không hợp lệ");
                }
                Account account = await _context.Accounts.Include(a => a.Permission).FirstOrDefaultAsync(a => a.Id == accountvm.Id);
                if (account == null)
                {
                    return NotFound();
                }
                if (accountvm.Password != accountvm.ConfirmPassword)
                {
                    ModelState.AddModelError("Password", "Mật khẩu mới không khớp.");
                    ModelState.AddModelError("ConfirmPassword", "Mật khẩu mới không khớp.");
                    return View(accountvm);
                }
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(accountvm.Password);
                account.Password = hashPassword;

                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Thay đổi mật khẩu tài khoản thành công!";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("Id", "Có lỗi bảo mật.");
            return View(accountvm);
        }
    }
}
