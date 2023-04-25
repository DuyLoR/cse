﻿using System.ComponentModel.DataAnnotations;
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
using Microsoft.EntityFrameworkCore.Storage;




namespace CSE_website.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly CSE_website.Data.CSEWebsiteContext _context;

        public DepartmentController(CSE_website.Data.CSEWebsiteContext context)
        {
            _context = context;
        }
           [BindProperty] public Department? Department { get; set; }
        public IActionResult Index()
        {

            List<Department> departments = _context.Departments.Include(d => d.Lecturers).ToList();
            if (TempData["AlertMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["AlertMessage"];
            }
            return View(departments);


        }
    

         public async Task<IActionResult> AddDepartment()
        {
            

            // Create data
            ModelState.Clear();
            if (Request.Method == HttpMethods.Post && (Department != null && ModelState.IsValid))
            {
                await _context.AddAsync(Department);
                // await LoadCustomDataAsync();
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Thêm bộ môn thành công!";
                return RedirectToAction("Index");
            }
            
            // View Form 
            ViewBag.Subjects = await _context.Subjects.ToListAsync();
            ViewBag.Lecturers = await _context.Lecturers.ToListAsync();
            ViewBag.FacultyOffices = await _context.FacultyOffices.ToListAsync();

            return View(Department);
        }


         public async Task<IActionResult> EditDepartment([RegularExpression("^[0-9]+$")][Required] int id)
        {
            
            var department = await _context.Departments
                .Include(dep => dep.Lecturers)
                .Include(dep => dep.FacultyOffice)
                .Include(dep => dep.Subjects)
                .FirstOrDefaultAsync(dep => dep.Id == id);

            if(ViewBag.ErrorMessage != null) return View(department);
            
            // Check if lecturer is not found
            if (department == null) return NotFound();

            // Check for validation errors on GET request
            if (Request.Method == HttpMethods.Get && !ModelState.IsValid) return BadRequest(ModelState);

            // Check for invalid requests
            if (Request.Method == HttpMethods.Post && (Department?.Id == null || id != Department.Id)) return NotFound();

            // Update data
            if (Request.Method == HttpMethods.Post && Department != null && ModelState.IsValid)
            {
                department.Subjects?.Clear();
                await _context.SaveChangesAsync();
                _context.Entry(department).State = EntityState.Detached;

                _context.Update(Department);
              
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Thay đổi thông tin bộ môn thành công!";

                return RedirectToAction("Index");
            }

            // View form
            ViewBag.Subjects = await _context.Subjects.ToListAsync();
            ViewBag.Lecturers = await _context.Lecturers.ToListAsync();
            ViewBag.FacultyOffices = await _context.FacultyOffices.ToListAsync();

            Department = department;
            // Department.SelectedLecturerId = department?.Lecturers?.Id;
            ViewBag.SelectedLecturerId = JsonConvert.SerializeObject(department?.Lecturers?.Select(lec => lec.Id).ToList());

            Department.SelectedFacultyOfficeId = department?.FacultyOffice?.Id;
           
            ViewBag.SelectedSubjectIdsListJson = JsonConvert.SerializeObject(department?.Subjects?.Select(sub => sub.Id).ToList());

            return View(Department);
        }
        
    

      
        [HttpGet]
      
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            Department department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Xóa bộ môn thành công!";
            return RedirectToAction(nameof(Index));
        }
       
    }
}
