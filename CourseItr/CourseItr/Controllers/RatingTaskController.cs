using CourseItr.Data;
using CourseItr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CourseItr.Controllers
{
    public class RatingTaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RatingTaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RatingTask
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RatingTask.Include(r => r.MTask).Include(r => r.RatingModel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RatingTask/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingTask = await _context.RatingTask
                .Include(r => r.MTask)
                .Include(r => r.RatingModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ratingTask == null)
            {
                return NotFound();
            }

            return View(ratingTask);
        }

        // GET: RatingTask/Create
        public IActionResult Create()
        {
            ViewData["MTaskId"] = new SelectList(_context.MTasks, "Id", "Name");
            ViewData["RatingModelId"] = new SelectList(_context.RatingModels, "Id", "Rating");
            return View();
        }

        // POST: RatingTask/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MTaskId,RatingModelId")] RatingTask ratingTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ratingTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MTaskId"] = new SelectList(_context.MTasks, "Id", "Name", ratingTask.MTaskId);
            ViewData["RatingModelId"] = new SelectList(_context.RatingModels, "Id", "Rating", ratingTask.RatingModelId);
            return View(ratingTask);
        }

        // GET: RatingTask/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingTask = await _context.RatingTask.FindAsync(id);
            if (ratingTask == null)
            {
                return NotFound();
            }
            ViewData["MTaskId"] = new SelectList(_context.MTasks, "Id", "Name", ratingTask.MTaskId);
            ViewData["RatingModelId"] = new SelectList(_context.RatingModels, "Id", "Rating", ratingTask.RatingModelId);
            return View(ratingTask);
        }

        // POST: RatingTask/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MTaskId,RatingModelId")] RatingTask ratingTask)
        {
            if (id != ratingTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ratingTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingTaskExists(ratingTask.Id))
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
            ViewData["MTaskId"] = new SelectList(_context.MTasks, "Id", "Name", ratingTask.MTaskId);
            ViewData["RatingModelId"] = new SelectList(_context.RatingModels, "Id", "Rating", ratingTask.RatingModelId);
            return View(ratingTask);
        }

        // GET: RatingTask/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingTask = await _context.RatingTask
                .Include(r => r.MTask)
                .Include(r => r.RatingModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ratingTask == null)
            {
                return NotFound();
            }

            return View(ratingTask);
        }

        // POST: RatingTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ratingTask = await _context.RatingTask.FindAsync(id);
            _context.RatingTask.Remove(ratingTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingTaskExists(int id)
        {
            return _context.RatingTask.Any(e => e.Id == id);
        }
    }
}
