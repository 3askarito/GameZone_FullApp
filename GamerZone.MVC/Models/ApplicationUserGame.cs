namespace GamerZone.MVC.Models
{
    public class ApplicationUserGame
    {
        public int GameId { get; set; }
        public Game Game { get; set; } = default!;
        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser ApplicationUser { get; set; } = default!;
    }
}
