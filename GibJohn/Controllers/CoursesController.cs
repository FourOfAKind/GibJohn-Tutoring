﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GibJohn.Data;
using GibJohn.Models;
using System.Security.Claims;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Win32;
using Microsoft.AspNetCore.Authorization;

namespace GibJohn.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        public CoursesController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Courses
        [Authorize(Roles = "Student,Management")]
        public async Task<IActionResult> Index()
        {
              return _context.Course != null ? 
                          View(await _context.Course.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Course'  is null.");
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyCourses()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var courseIds = await 
                _context.Registration
                .Where(r => r.UserId == userId)
                .Select(r => r.CourseId)
                .ToListAsync();

            var courses = await _context.Course
                .Where(c => courseIds.Contains(c.Id))
                .ToListAsync();

            return _context.Course != null ?
                View(courses) :
                Problem("Yeah");
        }

        // GET: Courses/Create
        [Authorize(Roles = "Management")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                if (course.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "uploaded");
                    course.ImagePath = Guid.NewGuid().ToString() + "_" + course.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, course.ImagePath);
                    course.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                _context.Add(course);
                await _context.SaveChangesAsync();
                // redirects to dashboard
                return RedirectToAction("Index", "Home");
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Management")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageURL")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Management")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Course == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Course'  is null.");
            }
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                _context.Course.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return (_context.Course?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> RegisterForCourse(IFormCollection collection)
        {
            int courseId = int.Parse(collection["id"].ToString());
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // make sure event exists
            Course? courseModel = await _context.Course.FirstOrDefaultAsync(e => e.Id == courseId);
            if (courseModel == null)
            {
                return NotFound();
            }

            // Check if a registration for the current user and event already exists
            var existingRegistration = _context.Registration.FirstOrDefault(r => r.CourseId == courseId && r.UserId == userId);
            if (existingRegistration != null)
            {
                // Already registered
                return RedirectToAction("Index", "Home");
            }

            var eventRegistration = new Registration
            {
                CourseId = courseId,
                UserId = userId,
                Date = DateTime.Now.ToString(),
            };

            _context.Registration.Add(eventRegistration);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Courses");
        }



    }
}
