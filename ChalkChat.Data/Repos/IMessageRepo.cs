using ChalkChat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChalkChat.Data.Repos
{
    public interface IMessageRepo
    {

        public Task<List<MessageModel>> GetAllAsync();

        public Task SaveChangesAsync();

        public Task<MessageModel> AddAsync(MessageModel newMessage);

        public Task UpdateUsernameAsync(string prevUsername, string newUsername);

        public Task<string> RemoveByIdAsync(int íd);

        public Task<MessageModel> GetIdAsync(int id);



    }
}
