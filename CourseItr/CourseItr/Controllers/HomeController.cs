using CourseItr.Data;
using CourseItr.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Korzh.EasyQuery.Linq;

namespace CourseItr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.MTasks.Include(m => m.MathTopic).Include(m => m.User);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> CheckAnswers()
        {
            var applicationDbContext = _context.MTasks.Include(m => m.MathTopic).Include(m => m.User);
            return View(await applicationDbContext.ToListAsync());
        }


    }
}
