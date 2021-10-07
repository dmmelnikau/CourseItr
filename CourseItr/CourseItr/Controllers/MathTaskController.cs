using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseItr.Data;
using CourseItr.Models;

namespace CourseItr.Controllers
{
    public class MathTaskController : Controller
    {
        private readonly ApplicationDbContext _context;

      

        public MathTaskController(ApplicationDbContext context)
        {
            _context = context;
           
        }

        // GET: MathTask
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MathTasks.Include(m => m.MathTopic);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MathTask/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mathTask = await _context.MathTasks
                .Include(m => m.MathTopic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mathTask == null)
            {
                return NotFound();
            }

            return View(mathTask);
        }

        // GET: MathTask/Create
        public IActionResult Create()
        {
           ViewBag.Topics = new SelectList(_context.MathTopics, "Id", "Name");
            return View();
        }

        // POST: MathTask/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MathTopicId")] MathTask mathTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mathTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Topics = new SelectList(_context.MathTopics, "Id", "Name", mathTask.Name);
            return View(mathTask);
        }

        // GET: MathTask/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mathTask = await _context.MathTasks.FindAsync(id);
            if (mathTask == null)
            {
                return NotFound();
            }
            ViewBag.Topics = new SelectList(_context.MathTopics, "Id", "Name", mathTask.Name);
            return View(mathTask);
        }

        // POST: MathTask/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MathTopicId")] MathTask mathTask)
        {
            if (id != mathTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mathTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MathTaskExists(mathTask.Id))
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
            ViewBag.Topics = new SelectList(_context.MathTopics, "Id", "Name", mathTask.Name);
            return View(mathTask);
        }

        // GET: MathTask/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mathTask = await _context.MathTasks
                .Include(m => m.MathTopic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mathTask == null)
            {
                return NotFound();
            }

            return View(mathTask);
        }

        // POST: MathTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mathTask = await _context.MathTasks.FindAsync(id);
            _context.MathTasks.Remove(mathTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MathTaskExists(int id)
        {
            return _context.MathTasks.Any(e => e.Id == id);
        }
    }
}
