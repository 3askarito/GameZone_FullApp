using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamerZone.MVC.Services
{
    public interface IDevicesServices 
    {
        IEnumerable<SelectListItem> GetDevices();
    }
}
