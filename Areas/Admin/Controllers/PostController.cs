using Microsoft.AspNetCore.Mvc;
using CSE_website.Models;
using CSE_website.Data;
using CSE_website.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CSE_website.Interfaces;

namespace CSE_website.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly CSEWebsiteContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IUploadFileService _uploadFileService;

        [BindProperty] public Post? Post { get; set; }

        public PostController(CSEWebsiteContext context, IWebHostEnvironment environment, IUploadFileService uploadFileService)
        {
            _context = context;
            _environment = environment;
            _uploadFileService = uploadFileService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddPost()
        {
            // Create data
            if (Request.Method == HttpMethods.Post && (Post != null && ModelState.IsValid))
            {
                await _context.AddAsync(Post);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            // View Form 
            ViewBag.Categories = await _context.Categories?.ToListAsync();

            return View(Post);
        }
        public IActionResult EditPost()
        {
            return View();
        }
    }
}
