using System.ComponentModel.DataAnnotations;
using System.Data;
using CSE_website.Constants;
using CSE_website.Data;
using CSE_website.Exceptions;
using CSE_website.Interfaces;
using CSE_website.Models;
using CSE_website.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CSE_website.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class LecturerController : Controller
    {
        private readonly CSEWebsiteContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IUploadFileService _uploadFileService;

        [BindProperty] public Lecturer? Lecturer { get; set; }

        public LecturerController(CSEWebsiteContext context, IWebHostEnvironment environment, IUploadFileService uploadFileService)
        {
            _context = context;
            _environment = environment;
            _uploadFileService = uploadFileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            try { RequestBodyValidate(); } catch (TooBigRequestBody ex) { return BadRequest(ex.Message); }

            // Create data
            if (Request.Method == HttpMethods.Post && (Lecturer != null && ModelState.IsValid))
            {
                await _context.AddAsync(Lecturer);
                await LoadCustomDataAsync();
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm giảng viên thành công!";

                return RedirectToAction("Index");
            }

            // View Form 
            ViewBag.Subjects = _context.Subjects?.ToList() ?? new List<Subject>() { };
            ViewBag.Departments = _context.Departments?.ToList() ?? new List<Department>() { };
            ViewBag.FacultyOffices = _context.FacultyOffices?.ToList() ?? new List<FacultyOffice>() { };

            return View(Lecturer);
        }

        public async Task<IActionResult> Update([RegularExpression("^[0-9]+$")][Required] int id)
        {
            try { RequestBodyValidate(); } catch (TooBigRequestBody ex) { return BadRequest(ex.Message); }

            var lecturer = await _context.Lecturers
                .Include(lec => lec.Department)
                .Include(lec => lec.FacultyOffice)
                .Include(lec => lec.Subjects)
                .FirstOrDefaultAsync(lec => lec.Id == id);

            if (ViewBag.ErrorMessage != null) return View(lecturer);

            // Check if lecturer is not found
            if (lecturer == null) return NotFound();

            // Check for validation errors on GET request
            if (Request.Method == HttpMethods.Get && !ModelState.IsValid) return BadRequest(ModelState);

            // Check for invalid requests
            if (Request.Method == HttpMethods.Post && (Lecturer?.Id == null || id != Lecturer.Id)) return NotFound();

            // Update data
            if (Request.Method == HttpMethods.Post && Lecturer != null && ModelState.IsValid)
            {
                lecturer.Subjects?.Clear();
                await _context.SaveChangesAsync();
                _context.Entry(lecturer).State = EntityState.Detached;

                _context.Update(Lecturer);
                await LoadCustomDataAsync();
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thay đổi thông tin giảng viên thành công!";

                return RedirectToAction("Index");
            }

            // View form
            ViewBag.Departments = await _context.Departments.ToListAsync();

            if (lecturer?.Department is not null)
            {
                ViewBag.Subjects = await _context.Subjects
                    .Where(sub =>
                        sub.Departments.Any(depart => depart.Id == lecturer.Department.Id)
                    )
                    .ToListAsync();
            }
            else
            {
                ViewBag.Subjects = new List<Subject>() { };
            }
            ViewBag.FacultyOffices = await _context.FacultyOffices.ToListAsync();

            Lecturer = lecturer;
            Lecturer.SelectedDepartmentId = lecturer?.Department?.Id;
            Lecturer.SelectedFacultyOfficeId = lecturer?.FacultyOffice?.Id;
            ViewBag.SelectedSubjectIdsListJson = JsonConvert.SerializeObject(lecturer?.Subjects?.Select(sub => sub.Id).ToList());

            return View(Lecturer);
        }

        public string GetAll()
        {
            List<Lecturer>? lecturers = _context.Lecturers?
                .Include(lecturer => lecturer.Department)
                .Include(lecturer => lecturer.FacultyOffice)
                .ToList();

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            return JsonConvert.SerializeObject(new { data = lecturers }, settings);
        }

        [HttpPost]
        public async Task<string?> GetAllSubjectsByDepartment([RegularExpression("^[0-9]+$")][Required] int id)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                var subjects = _context.Departments?
                    .Where(depart => depart.Id == id)
                    .SelectMany(depart => depart.Subjects)
                    .Select(sub => new
                    {
                        Id = sub.Id,
                        Name = sub.Name
                    }
                    );
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };
                return JsonConvert.SerializeObject(new { subjects }, settings);
            }
            return null;
        }

        [HttpPost]
        public async Task<string?> GetDepartmentsBySubject([RegularExpression("^[0-9]+$")][Required] int id)
        {
            if (ModelState.IsValid)
            {
                var departments = _context.Subjects?
                    .Where(sub => sub.Id == id)
                    .SelectMany(sub => sub.Departments)
                    .Select(sub => new
                    {
                        Id = sub.Id,
                        Name = sub.Name
                    }
                    );
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };
                return JsonConvert.SerializeObject(new { departments }, settings);
            }
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> Delete([RegularExpression("^[0-9]+$")][Required] int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var rows = await _context.Lecturers.Where(lec => lec.Id == id).ExecuteDeleteAsync();
            if (rows != 0)
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thay đổi thông tin giảng viên thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = SuccessMessConstants.UPDATE_UNSUCCESS;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Unactive([RegularExpression("^[0-9]+$")][Required] int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var lecturer = await _context.Lecturers.FirstOrDefaultAsync(lec => lec.Id == id);
                lecturer.Status = !lecturer.Status;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thay đổi thông tin giảng viên thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = SuccessMessConstants.UPDATE_UNSUCCESS;
            }
            return RedirectToAction("Index");
        }

        #region NonActions

        private async Task LoadCustomDataAsync()
        {
            // Load image path and upload file
            string? imageUrl = Request.Form["ImageUrl"];
            if (Lecturer?.ImageFile?.FileName is not null)
            {
                string path = await _uploadFileService.LoadFile(Lecturer.ImageFile);
                Lecturer.Image = path;
            }
            else if (imageUrl != null && imageUrl != "")
            {
                Lecturer.Image = imageUrl;
            }

            // Load subjects (Những môn giảng dạy)
            if (Lecturer.SelectedSubjectIdsList is not null && Lecturer.SelectedSubjectIdsList.Count != 0)
            {
                var subjects = await _context.Subjects.Where(
                        sub => Lecturer.SelectedSubjectIdsList.Contains(sub.Id)).ToListAsync();

                Lecturer.Subjects.AddRange(subjects);
            }

            // Load faculty (khoa)
            if (Lecturer.SelectedFacultyOfficeId != null)
            {
                Lecturer.FacultyOffice = await _context.FacultyOffices.Where(
                    fa => fa.Id == Lecturer.SelectedFacultyOfficeId).FirstOrDefaultAsync();
            }

            // Load department
            if (Lecturer.SelectedDepartmentId != null)
            {
                Lecturer.Department = await _context.Departments.SingleOrDefaultAsync(
                    department => department.Id == Lecturer.SelectedDepartmentId);
            }
        }

        private void RequestBodyValidate()
        {
            if (Request.ContentLength > 30000000) // 30MB
            {
                throw new TooBigRequestBody("File tải lên quá lớn, vui lòng kiểm tra lại thông tin!");
            }
        }
        #endregion
    }
}
