using System.ComponentModel.DataAnnotations;

namespace GamerZone.MVC.Models
{
    public class Game : BaseEntity
    {
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Cover { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public ICollection<GameDevice> GameDevices { get; set; } = new List<GameDevice>();
        public ICollection<ApplicationUserGame> ApplicationUserGames { get; set; } = new List<ApplicationUserGame>();
    }
}
