using System.ComponentModel.DataAnnotations;
using CSE_website.Constants;
using Microsoft.AspNetCore.Mvc;
using CSE_website.Models;
using CSE_website.Data;
using CSE_website.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CSE_website.Exceptions;
using System.Data;
using CSE_website.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CSE_website.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SubjectController : Controller
    {
        private readonly CSE_website.Data.CSEWebsiteContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IUploadFileService _uploadFileService;

        public SubjectController(CSE_website.Data.CSEWebsiteContext context, IWebHostEnvironment environment, IUploadFileService uploadFileService)
        {
            _context = context;
            _environment = environment;
            _uploadFileService = uploadFileService;
        }
        [BindProperty] public Subject? Subject { get; set; }
        
        public IActionResult Index()
        {
            List<Subject> subject = _context.Subjects.Include(s => s.Departments).ToList();
            ViewBag.Subjects = subject;
            if (TempData["AlertMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["AlertMessage"];
            }
            return View(subject);
        }

        public async Task<IActionResult> AddSubject()
        {


            // Create data
            if (Request.Method == HttpMethods.Post && (Subject != null && ModelState.IsValid))
            {
                await _context.AddAsync(Subject);
                await LoadCustomDataAsync();
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Thêm môn học thành công!";
                return RedirectToAction("Index");
            }

            // View Form 

            ViewBag.Departments = await _context.Departments.ToListAsync();


            return View(Subject);
        }
        public async Task<IActionResult> EditSubject([RegularExpression("^[0-9]+$")][Required] int id)
        {

            var subject = await _context.Subjects
                .Include(sup => sup.Departments)
              
                .FirstOrDefaultAsync(sup => sup.Id == id);

            if (ViewBag.ErrorMessage != null) return View(subject);

            // Check if lecturer is not found
            if (subject == null) return NotFound();

            // Check for validation errors on GET request
            if (Request.Method == HttpMethods.Get && !ModelState.IsValid) return BadRequest(ModelState);

            // Check for invalid requests
            if (Request.Method == HttpMethods.Post && (Subject?.Id == null || id != Subject.Id)) return NotFound();

            // Update data
            if (Request.Method == HttpMethods.Post && Subject != null && ModelState.IsValid)
            {
                subject.Departments?.Clear();
                await _context.SaveChangesAsync();
                _context.Entry(subject).State = EntityState.Detached;
                _context.Update(Subject);
                await LoadCustomDataAsync();
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Thay đổi thông tin môn học thành công!";

                return RedirectToAction("Index");
            }

            // View form

           
            ViewBag.Departments = await _context.Departments.ToListAsync();

         



            Subject = subject;
            // Department.SelectedLecturerId = department?.Lecturers?.Id;
            
            ViewBag.SelectedDepartmentIdsList = JsonConvert.SerializeObject(subject?.Departments?.Select(dep => dep.Id).ToList());


            return View(subject);
        }
        [HttpGet]

        public async Task<IActionResult> DeleteSubject(int id)
        {
            Subject subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Xóa môn học thành công!";
            return RedirectToAction(nameof(Index));
        }
        private async Task LoadCustomDataAsync()
        {
            // Load image path and upload file
            string? imageUrl = Request.Form["ImageUrl"];
            if (Subject?.OutlineFile?.FileName is not null && (imageUrl == null || imageUrl == ""))
            {
                string path = await _uploadFileService.LoadFile(Subject.OutlineFile);
                Subject.Outline = path;
            }
            if (imageUrl != null && imageUrl != "")
            {
                Subject.Outline = imageUrl;
            }
            // Load departments (Những bộ môn giảng dạy)
            if (Subject.SelectedDepartmentIdsList is not null && Subject.SelectedDepartmentIdsList.Count != 0)
            {
                var departments = await _context.Departments.Where(
                        dep => Subject.SelectedDepartmentIdsList.Contains(dep.Id)).ToListAsync();

                Subject.Departments.AddRange(departments);
            }

        }

        [HttpGet]
        public string GetAll()
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                var data = _context.Subjects?
                    .Include(sub => sub.Departments)
                    .ToList();
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };
                return JsonConvert.SerializeObject(new { data }, settings);
            }
            return null;
        }
        

        // [HttpPost]
        // public async Task<string?> GetAllLecturersByDepartment([RegularExpression("^[0-9]+$")][Required] int id)
        // {
        //     ModelState.Clear();
        //     if (ModelState.IsValid)
        //     {
        //         var lecturers = _context.Departments?
        //             .Where(depart => depart.Id == id)
        //             .SelectMany(depart => depart.Lecturers)
        //             .Select(lec => new
        //             {
        //                 Id = lec.Id,
        //                 Name = lec.Name
        //             }
        //             );
        //         var settings = new JsonSerializerSettings
        //         {
        //             ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //         };
        //         return JsonConvert.SerializeObject(new { lecturers }, settings);
        //     }
        //     return null;
        // }
        public IActionResult Download(string filePath)
        {
            var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot", filePath);

            var fileStream = new FileStream(path, FileMode.Open);

            return File(fileStream, "application/octet-stream", Path.GetFileName(path));
        }

        public async Task<IActionResult> GetDocumentUrl(string documentUrl)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(documentUrl);
            var contentStream = await response.Content.ReadAsStreamAsync();
            var encodedUrl = System.Net.WebUtility.UrlEncode(documentUrl);
            var previewUrl = $"https://view.officeapps.live.com/op/view.aspx?src={encodedUrl}";

            return Ok(new { Url = previewUrl });
        }


        private void RequestBodyValidate()
        {
            if (Request.ContentLength > 30000000) // 30MB
            {
                throw new TooBigRequestBody("File tải lên quá lớn, vui lòng kiểm tra lại thông tin!");
            }
        }
    }
}
