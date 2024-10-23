using GamerZone.MVC.Models;
using GamerZone.MVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamerZone.MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : Controller
    {
        public async Task<IActionResult> Index(int pg = 1)
        {

            //var users = await userManager.Users.Select(u => new UserViewModel {
            //    Id = u.Id,
            //    FirstName = u.FirstName,
            //    LastName = u.LastName,
            //    UserName = u.UserName,
            //    Email = u.Email,
            //    Roles = userManager.GetRolesAsync(u).Result
            //}).ToListAsync();
            //return View(users);
            const int PageSize = 5;
            if (pg < 1)
                pg = 1;
            var users = await userManager.Users.ToListAsync();
            var usersList = new List<UserViewModel>();
            foreach(var user in users)
            {
                var viewModel = new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = userManager.GetRolesAsync(user).Result
                };
                usersList.Add(viewModel);
            }
            Pager pager = new(usersList.Count(), pg, PageSize);
            var viewModels = usersList.Skip((pg - 1) * PageSize).Take(PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(viewModels);
        }
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var roles = await roleManager.Roles.ToListAsync();
            var viewModel = new UserRoleViewModel
            {
                Id = user.Id,
                Name = user.UserName,
                Roles = roles.Select(r => new RoleViewmodel
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    IsSelected = userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRoleViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(viewModel.Id);
            if(user is null) 
                return NotFound();
            var userRoles = await userManager.GetRolesAsync(user);
            foreach(var role in viewModel.Roles)
            {
                if (!role.IsSelected && userRoles.Any(r => r == role.RoleName))
                    await userManager.RemoveFromRoleAsync(user, role.RoleName);
                if (role.IsSelected && !userRoles.Any(r => r == role.RoleName))
                    await userManager.AddToRoleAsync(user, role.RoleName);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Add()
        {
            var roles = await roleManager.Roles.Select(r => new RoleViewmodel { RoleId = r.Id, RoleName = r.Name }).ToListAsync();
            var viewModel = new AddNewUserViewMode
            {
                Roles = roles
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddNewUserViewMode model)
        {
            if (!ModelState.IsValid)
                return View(model);            
            if (!model.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("Roles", "'Select at leastone Role!!!");
                return View(model);
            }
            if(await userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "Username is already exist!!!");
                return View(model);
            }
            if (await userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Username is already exist!!!");
                return View(model);
            }
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("Roles", error.Description);
                return View(model);
            }
            await userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName));
            
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var viewModel = new ProfileFormViewModel
            {
                Id = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
                return NotFound();
            var userWithSameUsername = await userManager.FindByNameAsync(model.UserName);
            if (userWithSameUsername != null && userWithSameUsername.Id != model.Id)
            {
                ModelState.AddModelError("UserName", "This Username Is Already assigned to anthoer User!");
                return View(model);
            }
            var userWithSameEmail = await userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail != null && userWithSameEmail.Id != model.Id)
            {
                ModelState.AddModelError("Email", "This Email Is Already assigned to anthoer User!");
                return View(model);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            await userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }

}
