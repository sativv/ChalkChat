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



        public EditUserModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public void OnGet()
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

        public async Task UpdateUser(string signedInUsername, string currentPassword, string newPassword)
        {
            var user = await userManager.FindByNameAsync(signedInUsername);
            if (user == null)
            {
                throw new ApplicationException($"That user was not found");
            }

            if (newUsername != null)
            {
                user.UserName = newUsername;
                var result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    throw new ApplicationException("Failed to update username");
                }
            }


            if (newPassword != null && currentPassword != null)
            {

                var validPassword = await userManager.CheckPasswordAsync(user, currentPassword);
                if (!validPassword)
                {
                    throw new ApplicationException("incorrect password");
                }

                var passResult = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (!passResult.Succeeded)
                {
                    throw new ApplicationException("Filaed to change password");
                }


            }

        }
    }
}
