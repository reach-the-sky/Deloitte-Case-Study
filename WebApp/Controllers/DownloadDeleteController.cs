using System;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    // DownloadController to serve the file user has requested
    public class DownloadDeleteController : Controller
    {
        private readonly ILogger<UploadController> _logger;
        private readonly FilesDbContext _context;

        // Get file from the server file storage as byte format
        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        public DownloadDeleteController(ILogger<UploadController> logger, FilesDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Request to download file
        [Authorize]
        public ActionResult DownloadFile(string filePath, string fileName)
        {
            try
            {
                string fullName = Path.Combine(Path.GetFullPath("UploadedFiles"), filePath);
                byte[] fileBytes = GetFile(fullName);

                return File(
                    fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch
            {
                return View(
                    new ErrorViewModel
                    {
                        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                    });
            }
        }

        // Request to delete a file
        [Authorize]
        public ActionResult DeleteFile(string filePath, int id)
        {
            try
            {
                string fullName = Path.Combine(Path.GetFullPath("UploadedFiles"), filePath);
                if (System.IO.File.Exists(fullName))
                {
                    System.IO.File.Delete(fullName);
                }
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                FileModel file;
                if (User.IsInRole("Admin"))
                {
                    file = new FileModel() { Id = id, };
                }
                else
                {
                    file = new FileModel() { UserId = userId, Id = id, };

                }
                _context.Remove(file);
                _context.SaveChanges();

                TempData["successMessage"] = "Deleted file";
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View(
                    new ErrorViewModel
                    {
                        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                    });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
