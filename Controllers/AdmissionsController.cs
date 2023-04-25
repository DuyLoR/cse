using CSE_website.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace CSE_website.Controllers
{
    public class AdmissionsController : Controller
    {
        private readonly CSEWebsiteContext _context;

        public AdmissionsController(CSEWebsiteContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {

            var postAdmissions = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Tuyển sinh" ||
                    p.Category.Name == "Đại học" ||
                    p.Category.Name == "Thạc sĩ" ||
                    p.Category.Name == "Tiến sĩ" ||
                    p.Category.Name == "Văn bằng 2")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postAdmissions);
        }
        public async Task<IActionResult> University(int page = 1, int pageSize = 10)
        {

            var postAdmissions = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Đại học")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postAdmissions);
        }
        public async Task<IActionResult> UniversityDetail(int id)
        {
            var postAdmissions = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
        public async Task<IActionResult> Master(int page = 1, int pageSize = 10)
        {

            var postAdmissions = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Thạc sĩ")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postAdmissions);
        }
        public async Task<IActionResult> MasterDetail(int id)
        {
            var postAdmissions = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
        public async Task<IActionResult> Doctor(int page = 1, int pageSize = 10)
        {

            var postAdmissions = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Tiến sĩ")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postAdmissions);
        }
        public async Task<IActionResult> DoctorDetail(int id)
        {
            var postAdmissions = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
        public async Task<IActionResult> Degree2(int page = 1, int pageSize = 10)
        {

            var postAdmissions = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Văn bằng 2")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postAdmissions);
        }
        public async Task<IActionResult> Degree2Detail(int id)
        {
            var postAdmissions = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
    }
}
