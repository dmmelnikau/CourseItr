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
    public class RatingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RatingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RatingViewModel
        public async Task<IActionResult> Index()
        {
            return View(await _context.RatingViewModels.ToListAsync());
        }

        // GET: RatingViewModel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingViewModel = await _context.RatingViewModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ratingViewModel == null)
            {
                return NotFound();
            }

            return View(ratingViewModel);
        }

        // GET: RatingViewModel/Create
        [HttpGet]
        public IActionResult Create()
        {
            RatingViewModel ratingViewModel = new RatingViewModel();
            return View(ratingViewModel);
        }

        // POST: RatingViewModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rating")] RatingViewModel ratingViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ratingViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ratingViewModel);
        }

        // GET: RatingViewModel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingViewModel = await _context.RatingViewModels.FindAsync(id);
            if (ratingViewModel == null)
            {
                return NotFound();
            }
            return View(ratingViewModel);
        }

        // POST: RatingViewModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rating")] RatingViewModel ratingViewModel)
        {
            if (id != ratingViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ratingViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingViewModelExists(ratingViewModel.Id))
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
            return View(ratingViewModel);
        }

        // GET: RatingViewModel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingViewModel = await _context.RatingViewModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ratingViewModel == null)
            {
                return NotFound();
            }

            return View(ratingViewModel);
        }

        // POST: RatingViewModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ratingViewModel = await _context.RatingViewModels.FindAsync(id);
            _context.RatingViewModels.Remove(ratingViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingViewModelExists(int id)
        {
            return _context.RatingViewModels.Any(e => e.Id == id);
        }
    }
}
