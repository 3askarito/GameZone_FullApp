using System.ComponentModel.DataAnnotations;

namespace GamerZone.MVC.ViewModel
{
    public class RoleFormViewModel
    {
        [Required, StringLength(265)]
        public string Name { get; set; }
    }
}
