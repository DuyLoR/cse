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

namespace CSE_website.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PositionController : Controller

    {
        private readonly CSE_website.Data.CSEWebsiteContext _context;
        public PositionController(CSE_website.Data.CSEWebsiteContext context)
        {
            _context = context;

        }
        [BindProperty] public Position? Position { get; set; }
        public IActionResult Index()
        {
            List<Position> position = _context.Positions.ToList();
            if (TempData["AlertMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["AlertMessage"];
            }
            return View(position);
        }
        public async Task<IActionResult> AddPosition()
        {


            // Create data
            if (Request.Method == HttpMethods.Post && (Position != null && ModelState.IsValid))
            {
                await _context.AddAsync(Position);

                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Thêm chức vụ thành công!";

                return RedirectToAction("Index");
            }

            // View Form 

            return View(Position);
        }
        public async Task<IActionResult> EditPosition([RegularExpression("^[0-9]+$")][Required] int id)
        {

            var position = await _context.Positions
                .FirstOrDefaultAsync(pos => pos.Id == id);

            if (ViewBag.ErrorMessage != null) return View(position);

            // Check if lecturer is not found
            if (position == null) return NotFound();

            // Check for validation errors on GET request
            if (Request.Method == HttpMethods.Get && !ModelState.IsValid) return BadRequest(ModelState);

            // Check for invalid requests
            if (Request.Method == HttpMethods.Post && (Position?.Id == null || id != Position.Id)) return NotFound();

            // Update data
            if (Request.Method == HttpMethods.Post && Position != null && ModelState.IsValid)
            {

                await _context.SaveChangesAsync();
                _context.Entry(position).State = EntityState.Detached;

                _context.Update(Position);

                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Thay đổi thông tin chức vụ thành công!";

                return RedirectToAction("Index");
            }

            // View form

            return View(position);
        }
        [HttpGet]

        public async Task<IActionResult> DeletePosition(int id)
        {
            Position position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Xóa chức vụ thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
