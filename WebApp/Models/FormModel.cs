using System.ComponentModel.DataAnnotations;
using WebApp.Utilities;

namespace WebApp.Models
{
    public class FormModel
    {
        [Required(ErrorMessage ="Please select a file first!")]
        [AllowedExtensions(extension:".pbix", ErrorMessage = "Please upload .pbix files only")]
        public List<IFormFile> files { get; set; }
        [Required]
        public DateTime metadata { get; set; }
    }
}
