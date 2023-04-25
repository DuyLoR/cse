using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace CSE_website.Controllers
{
    public class EnterpriseController : Controller
    {
        private readonly CSE_website.Data.CSEWebsiteContext _context;
        public EnterpriseController(CSE_website.Data.CSEWebsiteContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {

            var postEnterprises = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Doanh nghiệp" ||
                    p.Category.Name == "Việc làm" ||
                    p.Category.Name == "Thực tập tại doanh nghiệp")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postEnterprises);
        }
        public async Task<IActionResult> Partner(int page = 1, int pageSize = 10)
        {

            var postEnterprises = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Doanh nghiệp")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postEnterprises);
        }
        public async Task<IActionResult> PartnerDetail(int id)
        {
            var postEnterprises = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
        public async Task<IActionResult> Internship(int page = 1, int pageSize = 10)
        {

            var postEnterprises = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Việc làm")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postEnterprises);
        }
        public async Task<IActionResult> InternshipDetail(int id)
        {
            var postEnterprises = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
        public async Task<IActionResult> Job(int page = 1, int pageSize = 10)
        {

            var postEnterprises = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Việc làm")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postEnterprises);
        }
        public async Task<IActionResult> JobDetail(int id)
        {
            var postEnterprises = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
    }
}