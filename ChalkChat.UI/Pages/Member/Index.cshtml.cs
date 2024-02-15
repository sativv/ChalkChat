using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkChat.UI.Pages.Member
{
    public class IndexModel : PageModel

    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public IndexModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPost()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/account/login");
        }
    }
}
