using CourseItr.Data;
using CourseItr.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CourseItr.Controllers
{

    public class MathTaskController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration _configuration;
        public MathTaskController(ApplicationDbContext context, IConfiguration configuration, UserManager<User> _userManager)
        {
            _context = context;
            _configuration = configuration;
            userManager = _userManager;
        }

        // GET: MathTask
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MTasks.Include(m => m.MathTopic).Include(m => m.User).Where(a => a.User.UserName == User.Identity.Name); 
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> CheckAnswers()
        {
            var applicationDbContext = _context.MTasks.Include(m => m.MathTopic).Include(m => m.User);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: MathTask/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mathTask = await _context.MTasks
                .Include(m => m.MathTopic).Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mathTask == null)
            {
                return NotFound();
            }

            return View(mathTask);
        }
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {

            string blobstorageconnection = _configuration.GetValue<string>("blobstorage");
            byte[] dataFiles;
            // Retrieve storage account from connection string.
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
            // Create the blob client.
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container.
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("citrans");

            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            string systemFileName = file.FileName;
            await cloudBlobContainer.SetPermissionsAsync(permissions);
            await using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                dataFiles = target.ToArray();
            }
            // This also does not make a service call; it only creates a local object.
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(systemFileName);
            await cloudBlockBlob.UploadFromByteArrayAsync(dataFiles, 0, dataFiles.Length);

            return RedirectToAction(nameof(ShowAllBlobs));
        }

        public async Task<IActionResult> Download(string blobName)
        {
            CloudBlockBlob blockBlob;
            await using (MemoryStream memoryStream = new MemoryStream())
            {
                string blobstorageconnection = _configuration.GetValue<string>("blobstorage");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("citrans");
                blockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);
                await blockBlob.DownloadToStreamAsync(memoryStream);
            }

            Stream blobStream = blockBlob.OpenReadAsync().Result;
            return File(blobStream, blockBlob.Properties.ContentType, blockBlob.Name);
        }
        public async Task<IActionResult> DeleteUpload(string blobName)
        {
            string blobstorageconnection = _configuration.GetValue<string>("blobstorage");
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            string strContainerName = "citrans";
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
            var blob = cloudBlobContainer.GetBlobReference(blobName);
            await blob.DeleteIfExistsAsync();
            return RedirectToAction("ShowAllBlobs", "MTask");
        }
        public async Task<IActionResult> ShowAllBlobs()
        {
            string blobstorageconnection = _configuration.GetValue<string>("blobstorage");
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
            // Create the blob client.
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("citrans");
            CloudBlobDirectory dirb = container.GetDirectoryReference("citrans");


            BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(string.Empty,
                true, BlobListingDetails.Metadata, 100, null, null, null);
            List<FileData> fileList = new List<FileData>();

            foreach (var blobItem in resultSegment.Results)
            {
                var blob = (CloudBlob)blobItem;
                fileList.Add(new FileData()
                {
                    FileName = blob.Name,
                    FileSize = Math.Round((blob.Properties.Length / 1024f) / 1024f, 2).ToString(),
                    ModifiedOn = DateTime.Parse(blob.Properties.LastModified.ToString()).ToLocalTime().ToString()
                });
            }

            return View(fileList);
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
        public async Task<IActionResult> Create([Bind("Id,Name,Condition,MathTopicId,Option1,Option2,Option3,Correctians,UserId")] MTask mathTask, IFormFile files)
        {
            var user = userManager.FindByNameAsync(User.Identity.Name).Result;
            mathTask.UserId = user.Id;
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

            var mathTask = await _context.MTasks.FindAsync(id);
            if (mathTask == null)
            {
                return NotFound();
            }
            ViewBag.Topics = new SelectList(_context.MathTopics, "Id", "Name", mathTask.Name);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", mathTask.UserId);

            return View(mathTask);
        }

        // POST: MathTask/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Condition,MathTopicId,Option1,Option2,Option3,Correctians,UserId")] MTask mathTask)
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", mathTask.UserId);
            return View(mathTask);
        }

        // GET: MathTask/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mathTask = await _context.MTasks
                .Include(m => m.MathTopic).Include(m => m.User)
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
            var mathTask = await _context.MTasks.FindAsync(id);
            _context.MTasks.Remove(mathTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MathTaskExists(int id)
        {
            return _context.MTasks.Any(e => e.Id == id);
        }

    }
}
