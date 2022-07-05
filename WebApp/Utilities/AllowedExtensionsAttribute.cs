using System.ComponentModel.DataAnnotations;

namespace WebApp.Utilities
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string _extension;
        public AllowedExtensionsAttribute(string extension)
        {
            _extension = extension;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var files = value as List<IFormFile>;
            if (files != null)
            {
                foreach (IFormFile file in files)
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (!(extension == _extension))
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
