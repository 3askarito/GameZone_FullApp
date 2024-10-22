using System.ComponentModel.DataAnnotations;

namespace GamerZone.MVC.Models
{
    public class Device : BaseEntity
    {
        [MaxLength(100)]
        public string Icon { get; set; } = string.Empty;
        public ICollection<GameDevice> GameDevices { get; set; } = new List<GameDevice>();
    }
}
