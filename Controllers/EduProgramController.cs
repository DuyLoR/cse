using CSE_website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace CSE_website.Controllers
{
    public class EduProgramController : Controller
    {
        private readonly CSE_website.Data.CSEWebsiteContext _context;
        public EduProgramController(CSE_website.Data.CSEWebsiteContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, int pageSize = 10)
        {

            var postEduPrograms = _context.Posts
                .Where(p =>
                    (p.Category.Name == "Đào tạo" ||
                    p.Category.Name == "Mô tả chương trình đào tạo" ||
                    p.Category.Name == "Quy chế đào tạo")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedList(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postEduPrograms);
        }
        public async Task<IActionResult> Description(int page = 1, int pageSize = 10)
        {

            var postEduPrograms = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Mô tả chương trình đào tạo")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postEduPrograms);
        }
        public async Task<IActionResult> DescriptionDetail(int id)
        {

            var postEduPrograms = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
        public async Task<IActionResult> Regulations(int page = 1, int pageSize = 10)
        {

            var postEduPrograms = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Quy chế đào tạo")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postEduPrograms);
        }
        public async Task<IActionResult> RegulationsDetail(int id)
        {

            var postEduPrograms = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }

    }
}
