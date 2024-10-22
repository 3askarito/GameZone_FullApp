using GamerZone.MVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GamerZone.MVC.Services
{
    public class CategoriesServices(ApplicationDbContext dbContext) : ICategoriesServices
    {
        public IEnumerable<SelectListItem> GetCategories()
        {
            return dbContext.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).OrderBy(c => c.Text).AsNoTracking().ToList();
        }
    }
}
