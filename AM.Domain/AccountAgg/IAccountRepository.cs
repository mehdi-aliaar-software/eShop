using _0_Framework.Domain;
using AM.Application.Contracts.Account;

namespace AM.Domain.AccountAgg
{
    public interface IAccountRepository:IRepository<long,Account>
    {
        EditAccount GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}
