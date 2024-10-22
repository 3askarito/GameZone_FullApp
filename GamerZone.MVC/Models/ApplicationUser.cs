using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GamerZone.MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength]
        public string LastName { get; set; } = string.Empty;
        public byte[]? ProfileImage { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ApplicationUserGame> ApplicationUserGames { get; set; } = new List<ApplicationUserGame>();
    }
}
