using CourseItr.Data;
using CourseItr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CourseItr.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public UsersController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index(string searchString) {
            ViewData["CurrentFilter"] = searchString;
            var apliccationDb = from s in _context.Users
                                                  select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                apliccationDb = apliccationDb.Where(s => s.UserName.Contains(searchString));
            }
            return View(apliccationDb.ToList());
        }


        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
