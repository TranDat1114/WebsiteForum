// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using WebsiteForum.Data;

namespace WebsiteForum.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context,
            IWebHostEnvironment webHost)
        {
            _webHostEnvironment = webHost;
            _userManager = userManager;
            _signInManager = signInManager;
            _db = context;
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
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;
            ViewData["UserId"] = _userManager.GetUserId(User);
            var userss = await _db.ApplicationUsers.FindAsync(_userManager.GetUserId(User));
            ViewData["ProfilePicture"] = userss.ProfilePicture;
            Input = new InputModel
            {
                PhoneNumber = phoneNumber
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

        public async Task<IActionResult> OnPostInforAsync()
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

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUploadProfileImageAsync(string uId, IFormFile fileIMG)
        {
            if (!ModelState.IsValid || fileIMG == null)
            {
                return Page();

            }
            else
            {
                ClaimsIdentity? claimIdentity = (ClaimsIdentity)User.Identity;
                if (claimIdentity != null)
                {
                    var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;

                    if (userId != uId)
                    {
                        return RedirectToPage();

                    }
                    else
                    {
                        var user = _db.ApplicationUsers.Find(userId);

                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        if (fileIMG != null)
                        {
                            string folderName = "Profile";
                            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileIMG.FileName);
                            string productPath = Path.Combine(wwwRootPath, @$"assets\{folderName}\");

                            if (!string.IsNullOrEmpty(user.ProfilePicture))
                            {
                                //Xóa img cũ đi 
                                var oldIMGPath = Path.Combine(wwwRootPath, user.ProfilePicture.TrimStart('\\'));

                                if (System.IO.File.Exists(oldIMGPath))
                                {
                                    if (oldIMGPath != @$"\assets\Profile\defaul.png")
                                    {
                                        System.IO.File.Delete(oldIMGPath);
                                    }
                                }
                            }

                            using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                            {
                                fileIMG.CopyTo(fileStream);
                            }
                            user.ProfilePicture = @$"\assets\{folderName}\{fileName}";
                            _db.SaveChanges();

                        }
                    }
                }

                return RedirectToPage();
            }
        }
    }
}
