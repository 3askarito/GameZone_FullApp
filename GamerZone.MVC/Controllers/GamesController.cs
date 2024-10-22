using GamerZone.MVC.Data;
using GamerZone.MVC.Models;
using GamerZone.MVC.Services;
using GamerZone.MVC.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GamerZone.MVC.Controllers
{
    public class GamesController(UserManager<ApplicationUser> userManager,ICategoriesServices categoriesServices, IDevicesServices devicesServices, IGameServiecs gameServiecs) : Controller
    {
        public IActionResult Index()
        {
            var userID = userManager.GetUserId(User);
            if (userID == null)
                return NotFound();
            var Usergamesgames = gameServiecs.GetAll(userID);
            return View(Usergamesgames);
        }
        public IActionResult Details(int id)
        {
            var userID = userManager.GetUserId(User);
            if (userID == null)
                return NotFound();
            var game = gameServiecs.GetById(id, userID);
            if (game == null)
                return NotFound();
            return View(game);
        }
        [HttpGet]
        public IActionResult ManageGames()
        {
            var userID = userManager.GetUserId(User);
            if (userID == null)
                return NotFound();
            var Usergamesgames = gameServiecs.GetAll(userID);
            return View(Usergamesgames);

        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = categoriesServices.GetCategories(),
                Devices = devicesServices.GetDevices()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = categoriesServices.GetCategories();
                model.Devices = devicesServices.GetDevices();
                return View(model);
            }
            var UserId = userManager.GetUserId(User);
            await gameServiecs.Create(model, UserId);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userID = userManager.GetUserId(User);
            if (userID is null)
                return NotFound();
            var game = gameServiecs.GetById(id, userID);
            if (game is null)
                return NotFound();

            EditGameFromViewMmodel viewModle = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.DeviceIds,
                Categories = categoriesServices.GetCategories(),
                Devices = devicesServices.GetDevices(),
                CurrentCover = game.Cover
            };
            return View(viewModle);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFromViewMmodel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = categoriesServices.GetCategories();
                model.Devices = devicesServices.GetDevices();
                return View(model);
            }
            var UserId = userManager.GetUserId(User);
            var game= await gameServiecs.Upadete(model);
            if (game == null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var isDeleted = gameServiecs.Delete(Id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
