using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkChat.UI.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public bool IsAdmin { get; set; }
        public UserManager<IdentityUser> userManager { get; }

        public IndexModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task OnGetAsync()
        {

            // admin | Admin123!
            // hämta inloggade användaren
            IdentityUser? loggedInUser = await userManager.GetUserAsync(HttpContext.User);

            // kolla om dom har en admin roll

            IsAdmin = await userManager.IsInRoleAsync(loggedInUser, "Admin");
        }
        public async Task<IActionResult> OnPost()
        {
            bool adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            // om admin-rollen inte existerar redan
            if (!adminRoleExists)
            {
                // skapa ett admin-roll objekt
                IdentityRole adminRole = new()
                {
                    Name = "Admin"
                };

                // lägg till admin rollen i databasen
                var createAdminRoleResult = await roleManager.CreateAsync(adminRole);

                if (createAdminRoleResult.Succeeded)
                {
                    adminRoleExists = true;
                }
            }
            if (adminRoleExists)
            {
                // hämta den nuvarande inloggade användaren
                IdentityUser currentUser = await userManager.GetUserAsync(HttpContext.User);

                // lägg till admin-rollen till den nuvarande inloggande användaren

                var addToRoleResult = await userManager.AddToRoleAsync(currentUser, "Admin");

                if (addToRoleResult.Succeeded)
                {
                    return RedirectToPage("/Admin/Index");
                }
            }
            return Page();


        }

    }
}
