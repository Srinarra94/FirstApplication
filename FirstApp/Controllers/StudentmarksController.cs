using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstApp.Models;

namespace FirstApp.Controllers
{
    public class StudentmarksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentmarksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Studentmarks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StudentMarks.Include(s => s.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Studentmarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentmarks = await _context.StudentMarks
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentmarks == null)
            {
                return NotFound();
            }

            return View(studentmarks);
        }

        // GET: Studentmarks/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id");
            return View();
        }

        // POST: Studentmarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRecord([Bind("Id,StudentId,Grade,TotalMarks")] StudentMarks studentmarks)
        {
            if (ModelState.IsValid)
            {
                if (!StudentmarksExists(studentmarks.StudentId, studentmarks.Grade))
                {
                    _context.Add(studentmarks);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", studentmarks.StudentId);
            return View(studentmarks);
        }

        // GET: Studentmarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentmarks = await _context.StudentMarks.FindAsync(id);
            if (studentmarks == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", studentmarks.StudentId);
            return View(studentmarks);
        }

        // POST: Studentmarks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,Grade,TotalMarks")] StudentMarks studentmarks)
        {
            if (id != studentmarks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentmarks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", studentmarks.StudentId);
            return View(studentmarks);
        }

        // GET: Studentmarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentmarks = await _context.StudentMarks
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentmarks == null)
            {
                return NotFound();
            }

            return View(studentmarks);
        }

        // POST: Studentmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentmarks = await _context.StudentMarks.FindAsync(id);
            if (studentmarks != null)
            {
                _context.StudentMarks.Remove(studentmarks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentmarksExists(int studentId, string grade)
        {
            return _context.StudentMarks.Any(e => e.StudentId == studentId && e.Grade==grade);
        }
    }
}
