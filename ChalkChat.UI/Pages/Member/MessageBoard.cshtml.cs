using ChalkChat.App.Managers;
using ChalkChat.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkChat.UI.Pages.Member
{
    [BindProperties]
    public class MessageBoardModel : PageModel


    {

        public string MessageText { get; set; }
        public DateTime Date { get; set; }
        public string SignedInUsername { get; set; }


        public bool isAdmin { get; set; }

        public List<MessageModel> messageList { get; set; } = new();

        private readonly UserManager userManager;
        private readonly MessageManager messageManager;


        public MessageBoardModel(UserManager userManager, MessageManager messageManager)
        {
            this.userManager = userManager;
            this.messageManager = messageManager;

        }
        public async Task OnGet()
        {
            messageList = await messageManager.GetAllMessagesAsync();
            SignedInUsername = HttpContext.User.Identity.Name;

            isAdmin = await userManager.AdminCheckAsync(HttpContext);




        }



        public async Task<IActionResult> OnPostRemoveMessage(int id)
        {
            await RemoveMessageAsync(id);
            return RedirectToPage();

        }
        public async Task RemoveMessageAsync(int id)
        {
            await this.messageManager.RemoveMessage(id);
        }

        public async Task<IActionResult> OnPostAsync(string currentUser, string messageText)
        {
            await this.messageManager.AddMessageAsync(currentUser, messageText);
            return RedirectToPage();


        }
    }
}
