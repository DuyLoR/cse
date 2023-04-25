using CSE_website.Data;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CSE_website.Controllers
{
    public class ScienceAndTechnologyController : Controller
    {
        private readonly CSEWebsiteContext _context;

        public ScienceAndTechnologyController(CSEWebsiteContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {

            var postSciences = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Nghiên cứu khoa học" ||
                    p.Category.Name == "Công bố khoa học" ||
                    p.Category.Name == "Triển khai ứng dụng" ||
                    p.Category.Name == "Công bố khoa học" ||
                    p.Category.Name == "Các nhà nghiên cứu")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postSciences);
        }
        public async Task<IActionResult> ScientificPublication(int page = 1, int pageSize = 10)
        {

            var postSciences = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Công bố khoa học")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postSciences);
        }
        public async Task<IActionResult> ScientificPublicationDetail(int id)
        {
            var postSciences = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
        public async Task<IActionResult> ScientificPublicationDetail(int? id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            //Truyền dữ liệu của bài viết sang View thông qua ViewBag
            ViewBag.Title = post.Title;
            ViewBag.Content = post.Content;
            //và các thuộc tính khác cần thiết
            return View();
        }
        public async Task<IActionResult> ScienceResearchTopic(int page = 1, int pageSize = 10)
        {

            var postSciences = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Đề tài công bố khoa học")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postSciences);
        }
        public async Task<IActionResult> ScienceResearchTopicDetail(int id)
        {

            var postSciences = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
        public async Task<IActionResult> ApplicationDeployment(int page = 1, int pageSize = 10)
        {

            var postSciences = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Triển khai ứng dụng")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postSciences);
        }
        public async Task<IActionResult> ApplicationDeploymentDetail(int id)
        {
            var postSciences = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
        public async Task<IActionResult> Researchers(int page = 1, int pageSize = 10)
        {

            var postSciences = await _context.Posts
                .Where(p =>
                    (p.Category.Name == "Các nhà nghiên cứu")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedListAsync(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(postSciences);
        }
        public async Task<IActionResult> ResearcherDetail(int id)
        {
            var postSciences = await _context.Posts.FindAsync(id);
            return RedirectToAction("PostDetails", "Post", new { area = "" });
        }
    }
}
