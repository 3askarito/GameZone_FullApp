using System.ComponentModel.DataAnnotations;

namespace GamerZone.MVC.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
