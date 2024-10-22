using System.ComponentModel.DataAnnotations;

namespace GamerZone.MVC.Attributes
{
    public class AllowedExtenssionAttribute(string allowedExtension) : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                var extension = Path.GetExtension(file.FileName);
                var IsAllowed = allowedExtension.Split(",").Contains(extension, StringComparer.OrdinalIgnoreCase);
                if (!IsAllowed)
                    return new ValidationResult($"Only {allowedExtension} extensions are allowed!");
            }
            return ValidationResult.Success;
        }
    }
}
