
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;

namespace ChalkChat.UI.Pages.Member
{
    [BindProperties]
    public class EditUserModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;


        public string Password { get; set; }
        public string SignedInUsername { get; set; }

        public string? newUsername { get; set; }
        public string? newPassword { get; set; }
        public string ErrorMessage { get; set; }




        public EditUserModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        public async Task OnGetAsync()
        {


            //var signedInUser = userManager.GetUserAsync(HttpContext.User);
            SignedInUsername = HttpContext.User.Identity.Name;



        }

        public async Task OnPostAsync(string currentUser)
        {
            if (Password != null && newPassword != null || newUsername != null)
            {
                await UpdateUser(currentUser, Password, newPassword);
            }
        }

        public async Task<IActionResult> UpdateUser(string signedInUsername, string currentPassword, string newPassword)
        {
            var user = await userManager.FindByNameAsync(signedInUsername);
            if (user == null)
            {

                return Page();
            }

            if (newUsername != null)
            {
                user.UserName = newUsername;
                var result = await userManager.UpdateAsync(user);



                if (!result.Succeeded)
                {
                    ErrorMessage = "Failed to update username";
                    return Page();

                }

            }
            if (newPassword != null && currentPassword != null)
            {

                var validPassword = await userManager.CheckPasswordAsync(user, currentPassword);
                if (!validPassword)
                {
                    ErrorMessage = "That is not a valid password";
                    return Page();

                }

                var passResult = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (!passResult.Succeeded)
                {
                    ErrorMessage = "Failed to update password";
                    return Page();
                }
                ErrorMessage = "Password has been successfully updated!";
                return Page();


            }

            return Page();
        }
    }
}
