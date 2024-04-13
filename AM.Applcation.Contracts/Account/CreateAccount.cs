using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using AM.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace AM.Application.Contracts.Account
{
    public class CreateAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string FullName { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Username { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get;  set; }

        [Range(1,100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long RoleId { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Mobile { get;  set; }
        public IFormFile ProfilePhoto { get;  set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
