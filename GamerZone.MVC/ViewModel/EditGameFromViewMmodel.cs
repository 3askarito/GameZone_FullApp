using GamerZone.MVC.Attributes;

namespace GamerZone.MVC.ViewModel
{
    public class EditGameFromViewMmodel : GameFormViewModel
    {
        public int Id { get; set; }
        public string? CurrentCover { get; set; }
        [AllowedExtenssion(FileSettings.AllowedExtensions), MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
