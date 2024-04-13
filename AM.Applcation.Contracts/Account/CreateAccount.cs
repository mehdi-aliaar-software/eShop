﻿using Microsoft.AspNetCore.Http;

namespace AM.Application.Contracts.Account
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
