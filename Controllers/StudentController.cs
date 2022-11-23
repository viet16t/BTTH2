using DoanQuocVietBTTH2.Data;
using DoanQuocVietBTTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace DoanQuocVietBTTH2.Controllers

{
    public class StudentController : Controller{
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            ViewData["FacultyID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Faculty, "FacultyID","FacultyName");
            return View();
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.Students.ToListAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult>Create([Bind("StudentID,StudentName,FacultyID")] Student std)
        {
            if(ModelState.IsValid)
            {
                _context.Add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
            }
            ViewData["Faculty"] = new SelectList(_context.Faculty, "FacultyID", "FacultyName", std.FacultyID);
            return View(std);
        }
        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null)
            {
                return View("NotFound");
            }
            var student = await _context.Students.FindAsync(id);
            if(student == null)
            {
                return View("NotFound");
            }
            return View(student);
        }

        //POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentID,StudentName")] Student std)
        {
            if (id != std.StudentID)
            {
                return View("NotFound");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(std);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!StudentExists(std.StudentID))
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(std);
        }

        //Get: Product/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return View("NotFound");
            }
            var std = await _context.Students.FirstOrDefaultAsync(m => m.StudentID == id);
            if (std == null)
            {
                return View("NotFound");
            }
            return View(std);
        }
        
        //POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var std = await _context.Students.FindAsync(id);
            _context.Students.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}