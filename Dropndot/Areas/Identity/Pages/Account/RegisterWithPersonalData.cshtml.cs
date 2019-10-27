using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dropndot.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropndot.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterWithPersonalDataModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterWithPersonalDataModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            [StringLength(12, ErrorMessage = "The {0} must be at least 6 characters long.", MinimumLength = 6)]
            public string PhoneNumber { get; set; }

            [Display(Name = "Address")]
            public string Address { get; set; }

            [Required]
            [Display(Name = "Security Question")]
            public string SecurityQuestion { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least 6 characters long.", MinimumLength = 6)]
            [Display(Name = "Answer: ")]
            public string SecurityAnswer { get; set; }
        }

        public void OnGet()
        { }

        public async Task<IActionResult> OnPostAsync(string userId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (Input.SecurityAnswer == Input.FirstName || Input.SecurityAnswer == Input.LastName ||
                    Input.SecurityAnswer == Input.FirstName + " " + Input.LastName || 
                    Input.SecurityAnswer == user.Email)
                {
                    ModelState.AddModelError(string.Empty, "Security Answer must not Your Email or Name");
                }
                else
                {
                    user.FirstName = Input.FirstName;
                    user.LastName = Input.LastName;
                    user.Address = Input.Address;
                    user.SecurityQuestion = Input.SecurityQuestion;
                    user.SecurityAnswer = Input.SecurityAnswer;
                    user.PhoneNumber = Input.PhoneNumber;
                    user.SecurityStamp = Guid.NewGuid().ToString();

                    if (user.PhoneNumber != null)
                    {
                        user.PhoneNumberConfirmed = true;
                    }

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect("~/");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }             
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}