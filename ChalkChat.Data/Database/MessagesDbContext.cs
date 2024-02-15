using ChalkChat.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChalkChat.Data.Database
{
    public class MessagesDbContext : DbContext
    {
        public MessagesDbContext(DbContextOptions<MessagesDbContext> options) : base(options)
        {

        }
        public DbSet<MessageModel> Messages { get; set; }
    }
}
