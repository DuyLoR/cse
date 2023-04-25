using CSE_website.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using X.PagedList;

namespace CSE_website.Controllers
{
    public class IntroController : Controller
    {
        private readonly CSEWebsiteContext _context;

        public IntroController(CSEWebsiteContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DepartmentOverview()
        {
            return View();
        }
        public async Task<IActionResult> OrganizationalStructure()
        {
            var postOrganizationalStructure = await _context.Posts.Where(p => p.Category.Name == "Cơ cấu tổ chức").FirstOrDefaultAsync();
            return View(postOrganizationalStructure);
        }
        public async Task<IActionResult> Lecturer(int? page)
        {
            if (page == null) page = 1;
            var lecturers = _context.Lecturers?.Where(lec => lec.Status == true);
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            // Pass to partial view top expert sidebar
            ViewBag.Lecturers = lecturers?.ToList();
            ViewBag.UrlAction = "Lecturer";

            return View(lecturers?.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> LecturerDetails(int? id)
        {
            if (id is null) return RedirectToAction("Lecturer");

            var lecturer = await _context.Lecturers
                .Include(lec => lec.Subjects)
                .Include(lec => lec.Department)
                .Include(lec => lec.FacultyOffice)
                .FirstOrDefaultAsync(lec => lec.Id == id && lec.Status == true);

            if (lecturer is null) return NotFound();

            ViewBag.Lecturers = _context.Lecturers.Where(lec => lec.Status == true).Take(5).ToList();

            return View(lecturer);
        }
    }
}
