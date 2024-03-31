using _01_ShopQuery.Contracts.Slide;
using _01_ShopQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SM.Application;
using SM.Application.Contracts.Product;
using SM.Application.Contracts.ProductCategory;
using SM.Application.Contracts.ProductPicture;
using SM.Application.Contracts.Slide;
using SM.Domain.ProductAgg;
using SM.Domain.ProductCategoryAgg;
using SM.Domain.ProductPictureAgg;
using SM.Domain.SlideAgg;
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

            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository,  SlideRepository>();

            services.AddTransient<ISlideQuery, SlideQuery>();
            //services.AddTransient<ISlideQueryRepository, SlideQueryRepository>();

            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));

        }

    }
}
