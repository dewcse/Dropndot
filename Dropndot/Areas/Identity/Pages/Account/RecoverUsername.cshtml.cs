using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dropndot.Data;
using Dropndot.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropndot.Areas.Identity.Pages.Account
{
    public class RecoverUsernameModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public RecoverUsernameModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Email { get; set; }
            [Required]
            [Display(Name = "Security Question")]
            public string SecurityQuestion { get; set; }
            [Required]
            [Display(Name = "Security Answer")]
            public string SecurityAnswer { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string userEmail)
        {

            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return NotFound($"Unable to load user with Email '{userEmail}'.");
            }

            ViewData["SecurityQuestion"] = user.SecurityQuestion;
            ViewData["Email"] = userEmail;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return NotFound($"User Email is incorrect' {Input.SecurityAnswer} '.");
                }
                if (user.SecurityAnswer != Input.SecurityAnswer)
                {
                    return NotFound($"Security answer is incorrect' {Input.SecurityAnswer} '.");
                }

                if(user != null)
                {
                    return NotFound($"Your Username is ' {user.UserName} '.");
                }
                
            }

            return Page();
        }
    }
}