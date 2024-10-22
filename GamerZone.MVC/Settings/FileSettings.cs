namespace GamerZone.MVC.Settings
{
    public static class FileSettings
    {
        public const string ImagePath = "/assets/games/images";
        public const string AllowedExtensions = ".jpg,.jpeg,.png";
        public const int MaxFileSizeinMB = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeinMB * 1024 * 1024;
    }
}
