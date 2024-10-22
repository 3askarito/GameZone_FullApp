using GamerZone.MVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamerZone.MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController(RoleManager<IdentityRole> roleManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await roleManager.Roles.ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", await roleManager.Roles.ToListAsync());
            if(await roleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("Name", "Role is alraedy exists!");
                return View("Index", await roleManager.Roles.ToListAsync());
            }
            await roleManager.CreateAsync(new IdentityRole ( model.Name.Trim()));
            return RedirectToAction(nameof(Index));
        }
    }
}
