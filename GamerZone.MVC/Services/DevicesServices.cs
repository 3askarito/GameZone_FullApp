using GamerZone.MVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GamerZone.MVC.Services
{
    public class DevicesServices(ApplicationDbContext dbContext) : IDevicesServices
    {
        public IEnumerable<SelectListItem> GetDevices()
        {
            return dbContext.Devices.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).OrderBy(d => d.Text).AsNoTracking().ToList();
        }
    }
}
