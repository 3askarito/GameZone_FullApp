using GamerZone.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamerZone.MVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class UserController(UserManager<ApplicationUser> userManager) : ControllerBase
    {
        [HttpDelete]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            user.IsDeleted = true;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception();
            return Ok();
        }
    }
}
