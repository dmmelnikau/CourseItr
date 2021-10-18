using CourseItr.Data;
using CourseItr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CourseItr.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;

        public RatingController(ApplicationDbContext context, UserManager<User> usermanager)
        {
            _context = context;
            userManager = usermanager;
        }

        // GET: Rating
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RatingModels.Include(m => m.User).Where(a => a.User.UserName == User.Identity.Name);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rating/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingModel = await _context.RatingModels
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ratingModel == null)
            {
                return NotFound();
            }

            return View(ratingModel);
        }

        // GET: Rating/Create
        public IActionResult Create()
        {
            RatingModel model = new RatingModel();

            return View(model);
        }

        // POST: Rating/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rating,UserId")] RatingModel ratingModel)
        {
            var user = userManager.FindByNameAsync(User.Identity.Name).Result;
            ratingModel.UserId = user.Id;
            if (ModelState.IsValid)
            {
                _context.Add(ratingModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
         

            return View(ratingModel);
        }

        // GET: Rating/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingModel = await _context.RatingModels.FindAsync(id);
            if (ratingModel == null)
            {
                return NotFound();
            }
           
            return View(ratingModel);
        }

        // POST: Rating/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rating,UserId")] RatingModel ratingModel)
        {
            if (id != ratingModel.Id)
            {
                return NotFound();
            }
            var user = userManager.FindByNameAsync(User.Identity.Name).Result;
            ratingModel.UserId = user.Id;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ratingModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingModelExists(ratingModel.Id))
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
            
            return View(ratingModel);
        }

        // GET: Rating/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingModel = await _context.RatingModels
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ratingModel == null)
            {
                return NotFound();
            }

            return View(ratingModel);
        }

        // POST: Rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ratingModel = await _context.RatingModels.FindAsync(id);
            _context.RatingModels.Remove(ratingModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingModelExists(int id)
        {
            return _context.RatingModels.Any(e => e.Id == id);
        }
    }
}
