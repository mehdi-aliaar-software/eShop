using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM.Domain.RoleAgg;

namespace AM.Domain.AccountAgg
{
    public class Account:EntityBase
    {
        public string FullName { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }
        public string Mobile { get; private set; }
        public string ProfilePhoto { get; private set; }

        public Account(string fullName, string username, string password, long roleId, 
            string mobile, string profilePhoto)
        {
            FullName = fullName;
            Username = username;
            Password = password;
            RoleId = roleId;
            Mobile = mobile;
            ProfilePhoto = profilePhoto;
        }

        public void Edit(string fullName, string username, long roleId,
            string mobile, string profilePhoto)
        {
            FullName = fullName;
            Username = username;
            RoleId = roleId;
            Mobile = mobile;

            if (!string.IsNullOrWhiteSpace(profilePhoto))
            {
                ProfilePhoto = profilePhoto;
            }
        }

        public void ChangePassword(string password)
        {
            Password=password;
        }
    }
}
