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
    public class DownloadController : Controller
    {
        private readonly ILogger<UploadController> _logger;

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

        public DownloadController(ILogger<UploadController> logger)
        {
            _logger = logger;
        }

        // Request to download file
        [Authorize]
        public ActionResult DownloadFile(string filePath,string fileName)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
