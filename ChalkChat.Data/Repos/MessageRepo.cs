using ChalkChat.Data.Database;
using ChalkChat.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChalkChat.Data.Repos
{
    public class MessageRepo : IMessageRepo
    {

        private readonly MessagesDbContext context;

        public MessageRepo(MessagesDbContext context)
        {
            this.context = context;
        }


        public async Task<MessageModel> AddAsync(MessageModel newMessage)
        {
            //
            await context.AddAsync(newMessage);
            await SaveChangesAsync();
            return newMessage;

        }

        public async Task<List<MessageModel>> GetAllAsync()
        {
            // get all messages and put in descending order
            return await context.Messages.OrderByDescending(message => message.PostDate).ToListAsync();
        }

        public async Task<MessageModel?> GetIdAsync(int id)
        {
            return await context.Messages.FindAsync(id);
        }

        public async Task<string> RemoveByIdAsync(int id)
        {
            MessageModel? messageToRemove = await GetIdAsync(id);
            if (messageToRemove != null)
            {
                context.Messages.Remove(messageToRemove);
                await SaveChangesAsync();
                return "Message was removed!";
            }
            return "Message does not exist";
        }

        public Task<string> RemoveByIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            // save messagedbcontext
            await context.SaveChangesAsync();
        }

        public Task UpdateUsernameAsync(string prevUsername, string newUsername)
        {
            throw new NotImplementedException();
        }
    }
}
