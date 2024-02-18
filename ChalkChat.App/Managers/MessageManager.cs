using ChalkChat.Data.Models;
using ChalkChat.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChalkChat.App.Managers
{
    public class MessageManager
    {
        private readonly IMessageRepo repo;
        public MessageManager(IMessageRepo repo)
        {
            this.repo = repo;
        }


        public async Task<List<MessageModel>> GetAllMessagesAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<string> RemoveMessage(int id)
        {
            return await repo.RemoveByIdAsync(id);
        }

        public async Task<MessageModel?> AddMessageAsync(string? username, string? message)
        {
            if (username == null || message == null)
            {
                return null;
            }

            MessageModel newMessage = new()
            {
                Username = username,
                PostDate = DateTime.Now,
                Message = message
            };

            return await repo.AddAsync(newMessage);
        }

    }
}
