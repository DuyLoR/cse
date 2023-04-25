using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace CSE_website.Controllers
{
    public class ForeignCoopController : Controller
    {
        private readonly CSE_website.Data.CSEWebsiteContext _context;
        public ForeignCoopController(CSE_website.Data.CSEWebsiteContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, int pageSize = 10)
        {

            var postForeignCoops = _context.Posts
                .Where(p =>
                    (p.Category.Name == "Hợp tác đối ngoại" ||
                    p.Category.Name == "Chương trình hợp tác" ||
                    p.Category.Name == "Đối tác quốc tê")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedList(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postForeignCoops);

        }
        public async Task<IActionResult> CooperationProgram(int page = 1, int pageSize = 10)
        {

            var postForeignCoops = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Chương trình hợp tác")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postForeignCoops);
        }

        public async Task<IActionResult> CooperationProgramDetail(int id)
        {

            var postForeignCoops = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
        public async Task<IActionResult> InternationalPartners(int page = 1, int pageSize = 10)
        {

            var postForeignCoops = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Đối tác quốc tê")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postForeignCoops);
        }
        public async Task<IActionResult> InternationalPartnerDetail(int id)
        {

            var postForeignCoops = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
    }
}
