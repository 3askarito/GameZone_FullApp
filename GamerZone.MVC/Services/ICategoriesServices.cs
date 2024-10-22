using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamerZone.MVC.Services
{
    public interface ICategoriesServices
    {
        IEnumerable<SelectListItem> GetCategories();
    }
}
