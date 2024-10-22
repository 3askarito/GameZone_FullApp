// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using GamerZone.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GamerZone.MVC.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public const string AllowedExtensions = ".jpg,.jpeg,.png";
        public const int MaxFileSizeInMB = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// 
            [Required, Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Required]
            [Display(Name = "First Name")]
            public string LastName { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Profile Picture")]
            public byte[] ProfileImage { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = phoneNumber,
                ProfileImage = user.ProfileImage
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var firstName = user.FirstName;
            var lastName = user.LastName;
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.FirstName != firstName)
            {
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);
            }
            if (Input.LastName != lastName)
            {
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
            }
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            //if (Request.Form.Files.Count > 0)
            //{
            //    var file = Request.Form.Files.FirstOrDefault();

            //    //check file size and extension

            //    var extenssion = Path.GetExtension(file.FileName);
            //    var isAllowed = AllowedExtensions.Split(",").Contains(extenssion, StringComparer.OrdinalIgnoreCase);
            //    if (isAllowed && !(file.Length > MaxFileSizeInBytes))
            //    {
            //        using (var dataStream = new MemoryStream())
            //        {
            //            await file.CopyToAsync(dataStream);
            //            user.ProfileImage = dataStream.ToArray();
            //        }

            //        await _userManager.UpdateAsync(user);
            //    }
            //    else
            //    {
            //        StatusMessage = $"only {AllowedExtensions} extenssions and Photos Less Than {MaxFileSizeInMB}MB are allowed!";
            //        user.ProfileImage = null;
            //        await _userManager.UpdateAsync(user);
            //        return RedirectToPage();
            //    }
            //}
            if(Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
                var extensions = Path.GetExtension(file.FileName);
                var IsAllowedExtensions = AllowedExtensions.Split(",").Contains(extensions, StringComparer.OrdinalIgnoreCase);
                if(IsAllowedExtensions && !(file.Length > MaxFileSizeInBytes))
                {
                    using var datastream = new MemoryStream();
                    await file.CopyToAsync(datastream);
                    user.ProfileImage = datastream.ToArray();
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    StatusMessage = $"Only {AllowedExtensions} extinsseions are allowed with less than {MaxFileSizeInMB}MB";
                    user.ProfileImage = null;
                    await _userManager.UpdateAsync(user);
                    return RedirectToPage();
                }
            }
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}