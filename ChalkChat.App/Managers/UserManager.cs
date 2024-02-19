using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ChalkChat.App.Managers
{
    public class UserManager
    {

        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly MessageManager messageManager;

        public UserManager(SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, MessageManager messageManager)
        {
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.messageManager = messageManager;
        }


        public async Task<bool> AdminCheckAsync(HttpContext httpContext)
        {
            IdentityUser? userToCheck = await signInManager.UserManager.GetUserAsync(httpContext.User);
            if (userToCheck == null)
            {
                return false;
            }
            return await signInManager.UserManager.IsInRoleAsync(userToCheck, "Admin");
        }


        public async Task<string> ChangeUsernameAsync(string newUsername, string currentUsername, string password)
        {
            var user = await signInManager.UserManager.FindByNameAsync(currentUsername);

            if (user == null)
            {
                return "That user does not exist";
            }
            if (password == null)
            {
                return "Please enter your password to change username";
            }
            var validatePassword = await signInManager.UserManager.CheckPasswordAsync(user, password);
            if (newUsername != null && validatePassword)
            {
                user.UserName = newUsername;
                var result = await signInManager.UserManager.UpdateAsync(user);
            }
            else
            {
                return "Incorrect Password";
            }

            return $"Succesfully Updated Username to {newUsername}!";
        }

        public async Task<string> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            if (currentPassword == null || newPassword == null)
            {
                return "";
            }

            var user = await signInManager.UserManager.FindByNameAsync(username);
            if (user == null)
            {
                return "That user does not exist";
            }
            var validateCurrentPassword = await signInManager.UserManager.CheckPasswordAsync(user, currentPassword);
            if (!validateCurrentPassword)
            {
                return "Incorrect password";
            }
            var passwordResult = await signInManager.UserManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!passwordResult.Succeeded)
            {
                return "That is not a valid password";
            }

            return "Password has been updated successfully";
        }
    }
}

