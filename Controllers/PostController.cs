using CSE_website.Data;
using CSE_website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace CSE_website.Controllers
{
    public class PostController : Controller
    {
        private readonly CSEWebsiteContext _context;

        public PostController(CSEWebsiteContext context)
        {
            _context = context;
        }
        public IActionResult Activity(int page = 1, int pageSize = 10)
        {

            var posts = _context.Posts
                .Where(p =>
                    (p.Category.Name == "Sự kiện")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedList(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(posts);
        }
        public IActionResult News(int page = 1, int pageSize = 10)
        {

            var posts = _context.Posts
                .Where(p =>
                    (p.Category.Name == "Tin tức")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedList(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(posts);
        }
        public IActionResult Notification(int page = 1, int pageSize = 10)
        {

            var posts = _context.Posts
                .Where(p =>
                    (p.Category.Name == "Thông báo")
                )
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedList(page, pageSize);
            //sử dụng thẻ này để phân trang
            //<paging class="pagination-sm" paging-model="@Model.PageInfo" page-action="Index" page-route="page" max-pages="10"></paging>
            return View(posts);
        }
        public IActionResult PostDetails(Post post)
        {
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
    }
}
