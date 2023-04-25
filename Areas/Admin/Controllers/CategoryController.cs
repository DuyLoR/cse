using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CSE_website.Models;
using CSE_website.Data;
using CSE_website.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using CSE_website.Constants;
using CSE_website.Interfaces;

namespace CSE_website.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly CSEWebsiteContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IUploadFileService _uploadFileService;

        public CategoryController(CSEWebsiteContext context, IWebHostEnvironment environment, IUploadFileService uploadFileService)
        {
            _context = context;
            _environment = environment;
            _uploadFileService = uploadFileService;
        }

        #region DataAction
        public IActionResult Index()
        {
            return View();
        }

        public string GetAll()
        {
            List<Category>? categories = _context.Categories?
                .Include(cate => cate.Lecturer)
                .Include(c => c.CategoryParent)
                .Include(c => c.ChildCategoriesList)
                .ToList();

            var sortedCategories = GetSortedCategories(categories);

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            return JsonConvert.SerializeObject(new { data = sortedCategories }, settings);
        }
        #endregion

        #region AddAction
        public async Task<IActionResult> AddCategory([Bind] Category category)
        {
            ModelState.Clear();
            // View Form 
            if(Request.Method == HttpMethods.Get)
            {
                // Get Prepared Order
                category.Order = _context.Categories
                    .OrderByDescending(cate => cate.Order)
                    .Select(cate => cate.Order)
                    .FirstOrDefault() + 1;
                category.Level = "000" + category.Order.ToString();

                ViewBag.ManageLecturersList = await _context.Lecturers.ToListAsync();

                return View(category);
            }

            // Create data
            if (Request.Method == HttpMethods.Post && (category != null && ModelState.IsValid))
            {
                await _context.AddAsync(category);
                if(category.SelectedLecturerId is not null)
                {
                    var lecturer = _context.Lecturers.FirstOrDefault(lec => lec.Id == category.SelectedLecturerId);
                    category.Lecturer = lecturer;
                }
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm danh mục bài viết thành công!";

                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> AddChildCategory([RegularExpression("^[0-9]+$")][Required] int id)
        {
            ModelState.Clear();

            if(!ModelState.IsValid) return BadRequest("Có lỗi đã xảy ra vui lòng kiểm tra lại");


            // Get Prepared Order
            var parentCate = _context.Categories
                .Include(cate => cate.ChildCategoriesList)
                .FirstOrDefault(cate => cate.Id == id);

            var category = new Category();

            if(parentCate is null) return BadRequest();

            var childCatesList = parentCate.ChildCategoriesList;
            if(childCatesList.Count == 0) 
            {
                category.Order = 1; 
            }
            else
            {
                category.Order = childCatesList.Count + 1;
            }

            category.Level = parentCate.Level + "000" + category.Order.ToString();
            category.SelectedCategoryId = parentCate.Id;

            ViewBag.ManageLecturersList = await _context.Lecturers.ToListAsync();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddChildCategory([Bind][FromForm] Category category)
        {
            // Create data
            if (ModelState.IsValid)
            {
                await _context.AddAsync(category);
                if(category.SelectedLecturerId is not null)
                {
                    var lecturer = _context.Lecturers.FirstOrDefault(lec => lec.Id == category.SelectedLecturerId);
                    category.Lecturer = lecturer;
                }
                if(category.SelectedCategoryId is not null)
                {
                    category.CategoryParent = _context.Categories.FirstOrDefault(cate => cate.Id == category.SelectedCategoryId);
                }

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Thêm danh mục bài viết thành công!";
                
                return RedirectToAction("Index");
            }
            return View(category);
        }
        #endregion

        #region EditAction
        [HttpPost]
        public async Task<IActionResult> EditCategory([RegularExpression("^[0-9]+$")][Required] int id, [Bind]Category? category)
        {
            if(category?.Id == null || id != category.Id) return NotFound();

            var categoryData = await _context.Categories
                .Include(lec => lec.Lecturer)
                .Include(lec => lec.CategoryParent)
                .FirstOrDefaultAsync(cate => cate.Id == id);

            // Update data
            if (category != null && ModelState.IsValid)
            {
                _context.Entry(categoryData).State = EntityState.Detached;
                _context.Update(category);

                if(category.SelectedLecturerId is not null)
                {
                    var lecturer = _context.Lecturers.FirstOrDefault(lec => lec.Id == category.SelectedLecturerId);
                    category.Lecturer = lecturer;
                }

                // Update Parent Category
                category.CategoryParent = categoryData.CategoryParent;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thay đổi thông tin danh mục bài viết thành công!";

                return RedirectToAction("Index");
            }

            return BadRequest();
        }
        
        [HttpGet]
        public async Task<IActionResult> EditCategory([RegularExpression("^[0-9]+$")][Required] int id)
        {
            if(!ModelState.IsValid) return NotFound();

            var category = await _context.Categories
                .Include(lec => lec.Lecturer)
                .Include(lec => lec.CategoryParent)
                .FirstOrDefaultAsync(cate => cate.Id == id);

            // Check if category is not found
            if (category == null) return NotFound();

            // View form
            ViewBag.ManageLecturersList = await _context.Lecturers.ToListAsync();

            category.SelectedLecturerId = category?.Lecturer?.Id;

            return View(category);
        }
        #endregion

        #region UnactiveAction
        [HttpGet]
        public async Task<IActionResult> Unactive([RegularExpression("^[0-9]+$")][Required] int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(cate => cate.Id == id);
                category.Status = !category.Status;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = SuccessMessConstants.UPDATE_SUCCESS;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = SuccessMessConstants.UPDATE_UNSUCCESS;
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region DeleteAction
        [HttpGet]
        public async Task<IActionResult> Delete([RegularExpression("^[0-9]+$")][Required] int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var rows = await _context.Categories.Where(cate => cate.Id == id).ExecuteDeleteAsync();
            if (rows != 0)
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa danh mục thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = SuccessMessConstants.UPDATE_UNSUCCESS;
            }
            return RedirectToAction("Index");
        }
        #endregion 

        #region Api
        public string GetParentWithChildCategoriesByJson()
        {
            var categories = _context.Categories
                .Where(cate => cate.CategoryParent == null)
                .Include(cate => cate.ChildCategoriesList).ToList();

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            return JsonConvert.SerializeObject(new { categories }, settings);
        }
        #endregion 

        #region NonAction
        [NonAction]
        public List<Category> GetSortedCategories(List<Category> categories, int? parentId = null)
        {
            var sortedCategories = new List<Category>();
            foreach (var category in categories.Where(c => c.CategoryParent?.Id == parentId))
            {
                sortedCategories.Add(category);
                sortedCategories.AddRange(GetSortedCategories(categories, category.Id));
            }
            return sortedCategories;
        }
        #endregion
    }
}
