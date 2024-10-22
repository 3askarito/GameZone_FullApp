using GamerZone.MVC.Attributes;
using GamerZone.MVC.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GamerZone.MVC.ViewModel
{
    public class CreateGameFormViewModel : GameFormViewModel
    {
        [AllowedExtenssion(FileSettings.AllowedExtensions), MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
