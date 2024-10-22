using System.ComponentModel.DataAnnotations;

namespace GamerZone.MVC.Attributes
{
    public class MaxFileSizeAttribute(int allowedFileSize) : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                if (file.Length > allowedFileSize)
                    return new ValidationResult($"Maximum allowed file size is {allowedFileSize} bytes");
            }
            return ValidationResult.Success;
        }
    }
}
