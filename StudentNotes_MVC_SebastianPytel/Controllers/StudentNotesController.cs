using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentNotes_MVC_SebastianPytel.Data;
using StudentNotes_MVC_SebastianPytel.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace StudentNotes_MVC_SebastianPytel.Controllers
{
    [Authorize]
    public class StudentNotesController : Controller
    {
        private readonly StudentNotesContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public StudentNotesController(StudentNotesContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: StudentNotes
        public async Task<IActionResult> Index()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(await _context.StudentNote.Where(a => a.UserId == userId).OrderByDescending(d => d.ModifiedDate).ToListAsync());
        }
        
        public async Task<IActionResult> IndexAjaxModified()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return PartialView("IndexAjax", await _context.StudentNote.Where(a => a.UserId == userId).OrderByDescending(d => d.ModifiedDate).ToListAsync());
        }

        public async Task<IActionResult> IndexAjaxCreated()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return PartialView("IndexAjax", await _context.StudentNote.Where(a => a.UserId == userId).OrderByDescending(d => d.CreatedDate).ToListAsync());
        }

        // GET: StudentNotes/Details/5
        public async Task<IActionResult> DetailsAjax(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentNote = await _context.StudentNote
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentNote == null)
            {
                return NotFound();
            }

            //check if note belongs to user
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (studentNote.UserId != userId)
            {
                return NotFound();
            }

            return PartialView(studentNote);
        }

        // GET: StudentNotes/Create
        public IActionResult CreateAjax()
        {
            return PartialView();
        }

        // POST: StudentNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteLabel, Note")] StudentNote studentNote)
        {
            if (ModelState.IsValid)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                studentNote.UserId = userId;
                studentNote.CreatedDate = DateTime.UtcNow;
                studentNote.ModifiedDate = DateTime.UtcNow;
                _context.Add(studentNote);
                await _context.SaveChangesAsync();
                return Json(new { noteId = studentNote.Id, noteLabel = studentNote.NoteLabel });
                //return RedirectToAction(nameof(Index));
            }
            return View(studentNote);
        }

        // GET: StudentNotes/Edit/5
        public async Task<IActionResult> EditAjax(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var studentNote = await _context.StudentNote.FindAsync(id);
            //if (studentNote == null)
            //{
            //    return NotFound();
            //}
            var studentNote = await _context.StudentNote
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentNote == null)
            {
                return NotFound();
            }

            //check if note belongs to user
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (studentNote.UserId != userId)
            {
                return NotFound();
            }

            return PartialView(studentNote);
        }

        // POST: StudentNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id, NoteLabel, Note")] StudentNote studentNote)
        public async Task<IActionResult> Edit(int id, [Bind("Id, NoteLabel, Note")] StudentNote studentNote)
        {
            //if (id != studentNote.Id)
            //{
            //    return NotFound();
            //}

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var noteFromDb = _context.StudentNote.Where(a => a.Id == studentNote.Id).AsNoTracking().Single();

            if (noteFromDb.UserId != userId)
            {
                return NotFound();
            }

            //set right UserId for note
            studentNote.UserId = userId;
            //set created and modified date
            studentNote.CreatedDate = noteFromDb.CreatedDate;
            studentNote.ModifiedDate = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Json(new { noteId = studentNote.Id, success = false });
                }
                return Json(new { noteId = id, noteLabel = studentNote.NoteLabel, success = true });
                //return RedirectToAction(nameof(Index));
            }
            return Json(new { noteId = studentNote.Id, success = false });
            //return View(studentNote);
        }
        
        // POST: StudentNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentNote = await _context.StudentNote.FindAsync(id);

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (studentNote.UserId != userId)
            {
                return NotFound();
            }

            _context.StudentNote.Remove(studentNote);
            await _context.SaveChangesAsync();
            return Json(new { noteId = id, noteLabel = studentNote.NoteLabel });
            //return RedirectToAction(nameof(Index));
        }
    }
}
