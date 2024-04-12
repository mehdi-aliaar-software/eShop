using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Applcation.Contracts.Account
{
    public class CreateAccount
    {
        public string FullName { get;  set; }
        public string Username { get;  set; }
        public string Password { get;  set; }
        public long RoleId { get;  set; }
        public string Mobile { get;  set; }
        public IFormFile ProfilePhoto { get;  set; }
    }
}
