using ChalkChat.Data.Database;
using ChalkChat.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;

namespace ChalkChat.UI.Pages.Member
{
    [BindProperties]
    public class DetailsModel : PageModel


    {

        public string MessageText { get; set; }
        public DateTime Date { get; set; }
        public string SignedInUsername { get; set; }

        public List<MessageModel> messageList { get; set; } = new();



        private readonly MessagesDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManage;

        public DetailsModel(MessagesDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManage)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManage = signInManage;
        }
        public async void OnGet()
        {

            messageList = context.Messages.OrderByDescending(msg => msg.PostDate).ToList();

            SignedInUsername = HttpContext.User.Identity.Name;
        }

        public IActionResult OnPostRemoveMessage(int id)
        {
            RemoveMessage(id);
            return RedirectToPage();

        }
        public void RemoveMessage(int id)
        {
            var messageToDelete = context.Messages.FirstOrDefault(msg => msg.Id == id);
            if (messageToDelete != null)
            {
                context.Messages.Remove(messageToDelete);
                context.SaveChanges();

            }


        }

        public IActionResult OnPost(string currentUser)
        {
            if (MessageText != null)
            {
                MessageModel newMessage = new()
                {
                    Message = MessageText,
                    PostDate = DateTime.Now,
                    Username = currentUser,

                };

                context.Messages.Add(newMessage);
                context.SaveChanges();


            }
            return RedirectToPage();


        }
    }
}
