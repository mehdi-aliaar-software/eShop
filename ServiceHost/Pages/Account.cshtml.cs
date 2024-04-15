using AM.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        [TempData]
        public string dummy { get; set; }

        //[TempData]
        public string LoginMessage { get; set; }

        [TempData]
        public string RegisterMessage { get; set; }
        private readonly IAccountApplication _accountApplication;

        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
            LoginMessage = " dummy loginMessage";
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(Login command)
        {
            dummy = "welcome to new ambi";

            var result = _accountApplication.Login(command);
            if (result.IsSucceeded)
            {
                return RedirectToPage("/Index");
            }

            LoginMessage = result.Message;
            return RedirectToPage("/Account");
        }

        public IActionResult OnPostLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }
        public IActionResult OnPostRegister(RegisterAccount command)
        {
            var result= _accountApplication.Register(command);
            if (result.IsSucceeded)
            {
                return RedirectToPage("/Account");
            }
            RegisterMessage = result.Message;
            return RedirectToPage("/Account");
        }
    }
}
