using System.Runtime.InteropServices;
using AM.Domain.AccountAgg;
using AM.Infrastructure.EfCore.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AM.Infrastructure.EfCore
{
    public class AccountContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public AccountContext(DbContextOptions<AccountContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(AccountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
