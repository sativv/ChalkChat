using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChalkChat.Data.Models
{
    public class MessageModel
    {

        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime PostDate { get; set; }

        public string Username { get; set; }
    }
}
