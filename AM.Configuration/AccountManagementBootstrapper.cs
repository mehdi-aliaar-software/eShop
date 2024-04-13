using AM.Application;
using AM.Application.Contracts.Account;
using AM.Domain.AccountAgg;
using AM.Infrastructure.EfCore;
using AM.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AM.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IAccountRepository,  AccountRepository>();
            
            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionString));
        }

    }
}
