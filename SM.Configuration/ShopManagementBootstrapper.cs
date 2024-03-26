using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SM.Application;
using SM.Application.Contracts.ProductCategory;
using SM.Domain.ProductCategoryAgg;
using SM.Infrastructure.EfCore;
using SM.Infrastructure.EfCore.Repository;
//using SM.Domain.ProductCategoryAgg;

namespace SM.Configuration
{
    public static class ShopManagementBootstrapper
    {
        //public static void Configure(IServiceCollection services, string connectionString)
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));

        }

    }
}
