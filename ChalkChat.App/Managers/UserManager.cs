using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
