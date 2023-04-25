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
using Microsoft.AspNetCore.Authorization;
using CSE_website.Interfaces;

namespace CSE_website.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PartnerController : Controller
    
    {
        
        private readonly CSE_website.Data.CSEWebsiteContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IUploadFileService _uploadFileService;
         public PartnerController(CSE_website.Data.CSEWebsiteContext context, IWebHostEnvironment environment, IUploadFileService uploadFileService)
        {
            _context = context;
            _environment = environment;
            _uploadFileService = uploadFileService;
        }
        [BindProperty] public Partner? Partner { get; set; }
        public IActionResult Index()
        {
             List<Partner> partner = _context.Partners.Include(p => p.Category).ToList();
            if (TempData["AlertMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["AlertMessage"];
            }
            return View(partner);
        }
          public async Task<IActionResult> AddPartner()
        {
           

            // Create data
            if (Request.Method == HttpMethods.Post && (Partner != null && ModelState.IsValid))
            {
                await _context.AddAsync(Partner);
                await LoadCustomDataAsync();
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Thêm đối tác thành công!";
                
                return RedirectToAction("Index");
            }
            
            // View Form 
            ViewBag.Category = await _context.Categories.ToListAsync();

            return View(Partner);
        }
        public async Task<IActionResult> EditPartner ([RegularExpression("^[0-9]+$")][Required] int id)
        {
            
            var partner = await _context.Partners
                .Include(par => par.Category)
                .FirstOrDefaultAsync(par => par.Id == id);

            if(ViewBag.ErrorMessage != null) return View(partner);
            
            // Check if lecturer is not found
            if (partner == null) return NotFound();

            // Check for validation errors on GET request
            if (Request.Method == HttpMethods.Get && !ModelState.IsValid) return BadRequest(ModelState);

            // Check for invalid requests
            if (Request.Method == HttpMethods.Post && (Partner?.Id == null || id != Partner.Id)) return NotFound();

            // Update data
            if (Request.Method == HttpMethods.Post && Partner != null && ModelState.IsValid)
            {
               
                await _context.SaveChangesAsync();
                _context.Entry(partner).State = EntityState.Detached;

                _context.Update(Partner);
              
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Thay đổi thông tin đối tác thành công!";

                return RedirectToAction("Index");
            }

            // View form
            ViewBag.Category = await _context.Categories.ToListAsync();

            Partner = partner;
            // Department.SelectedLecturerId = department?.Lecturers?.Id;
            

            Partner.SelectedCategoryId = partner?.Category?.Id;
           
           

            return View(Partner);
        }

       

        [HttpGet]
      
        public async Task<IActionResult> DeletePartner(int id)
        {
            Partner partner = await _context.Partners.FindAsync(id);
            if (partner == null)
            {
                return NotFound();
            }

            _context.Partners.Remove(partner);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Xóa đối tác thành công!";
            return RedirectToAction(nameof(Index));
        }
          [HttpGet]
          public string GetAll()
          
        {
            ModelState.Clear();
             if (ModelState.IsValid)
            {
                var partners = _context.Partners?
                .Include(partner => partner.Category)
                .ToList();

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
             return JsonConvert.SerializeObject(new { data = partners }, settings);
            }
           return null;
            
        }

        [HttpGet]
        public async Task<IActionResult> Unactive([RegularExpression("^[0-9]+$")][Required] int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var partner = await _context.Partners.FirstOrDefaultAsync(par => par.Id == id);
                partner.Status = !partner.Status;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = SuccessMessConstants.UPDATE_SUCCESS;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = SuccessMessConstants.UPDATE_UNSUCCESS;
            }
            return RedirectToAction("Index");
        }

         private async Task LoadCustomDataAsync()
        {
            // Load image path and upload file
            string? imageUrl = Request.Form["ImageUrl"];
            if (Partner?.ImageFile?.FileName is not null && (imageUrl == null || imageUrl == ""))
            {
                string path = await _uploadFileService.LoadFile(Partner.ImageFile);
                Partner.Logo = path;
            }
            if (imageUrl != null && imageUrl != "")
            {
                Partner.Logo = imageUrl;
            }
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
