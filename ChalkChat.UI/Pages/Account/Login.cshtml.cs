using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkChat.UI.Pages.Account
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public string ErrorMessage { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public LoginModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public void OnGet()
        {


        }

        public async Task<IActionResult> OnPostAsync()
        {
            IdentityUser userToLogin = await userManager.FindByNameAsync(Username);
            if (userToLogin != null)
            {
                var signInResult = await signInManager.PasswordSignInAsync(userToLogin, Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToPage("/Member/messageboard");
                }

            }

            ErrorMessage = "Incorrect username or password";
            return Page();
        }
    }
}
