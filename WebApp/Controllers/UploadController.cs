using System;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UploadController : Controller
    {
        private readonly FilesDbContext _context;
        private readonly ILogger<UploadController> _logger;

        public UploadController(FilesDbContext context, ILogger<UploadController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // upload helper to upload a file to local file storage
        private async Task<bool> UploadFileHelper(List<IFormFile> files, string userId, FormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var filePaths = new List<string>();
                    foreach (IFormFile formFile in files)
                    {
                        if (formFile.Length > 0)
                        {
                            string filePath = Path.Combine(Path.GetFullPath("UploadedFiles"), Guid.NewGuid().ToString() + Path.GetFileName(formFile.FileName));
                            filePaths.Add(filePath);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                            }

                            _logger.LogTrace(message: $"Logging User Identity: {User.Identity?.ToString()}");
                            var f = new WebApp.Models.FileModel()
                            {
                                Name = formFile.FileName,
                                Extension = formFile.ContentType,
                                UpdatedDate = model.metadata,
                                Size = (int)formFile.Length,
                                Path = Path.GetFileName(filePath),
                                UserId = userId,
                            };
                            _context.Add(f);
                            _context.SaveChanges();
                        }
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            } 
            else
            {
                return false;
            }
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UploadFile(FormModel model)
        {
            try
            {
                List<IFormFile> files = model.files;
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var status = await UploadFileHelper(files,userId, model);
                if(status)
                {
                    return View();
                }
                return View(model);
            }
            catch
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
