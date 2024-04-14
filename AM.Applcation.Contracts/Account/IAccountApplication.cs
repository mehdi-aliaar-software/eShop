using _0_Framework.Application;

namespace AM.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Register(RegisterAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePassword command);
        OperationResult login(Login command);
        EditAccount GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        void Logout();
    }
}

