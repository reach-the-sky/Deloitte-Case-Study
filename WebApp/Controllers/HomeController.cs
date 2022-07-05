using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApp.Data;
using WebApp.Models;
using File = WebApp.Models.FileModel;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FilesDbContext _files;

        public HomeController(FilesDbContext files, ILogger<HomeController> logger)
        {
            _logger = logger;
            _files = files;
        }

        public IActionResult Index()
        {
            // Filter the files for a the logged in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<File>? FileList = _files.File?.Where(x => x.UserId == userId);
            return View(FileList);
        }

        // Show specific file metadata based on the file id
        [Authorize]
        public IActionResult FileMetadata(int file)
        {
            // Get the metadata of a file the user has access to
            IEnumerable<File>? CurrentFile = 
                _files.File?.Where(
                    x => x.Id == file 
                    && x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            return PartialView("_FileMetadata", CurrentFile);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}