using _0_Framework.Domain;
using AM.Application.Contracts.Account;
using System.Linq.Expressions;

namespace AM.Domain.AccountAgg
{
    public interface IAccountRepository:IRepository<long,Account>
    {
        Account GetByUsername(string username);
        EditAccount GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}
