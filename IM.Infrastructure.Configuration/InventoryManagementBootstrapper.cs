using IM.Application;
using IM.Application.Contracts.Inventory;
using IM.Domain.InventoryAgg;
using IM.Infrastructure.EfCore;
using IM.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Infrastructure.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInventoryApplication, InventoryApplication>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();



            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
