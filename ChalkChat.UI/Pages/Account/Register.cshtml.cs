using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkChat.UI.Pages.Account
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public string? Username { get; set; }
        public string? Password { get; set; }

        public RegisterModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            IdentityUser newUser = new()
            {
                UserName = Username,
            };

            var createUserResult = await userManager.CreateAsync(newUser, Password);

            if (createUserResult.Succeeded)
            {
                IdentityUser? userToLogin = await userManager.FindByNameAsync(Username);

                var signInResult = await signInManager.PasswordSignInAsync(userToLogin, Password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToPage("/Member/Index");
                }
                else
                {
                    // fel lösenord
                }
            }
            else
            {

            }
            return Page();


        }
    }
}
