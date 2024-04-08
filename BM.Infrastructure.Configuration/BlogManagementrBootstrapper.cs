using BM.Application;
using BM.Application.Contracts.Article;
using BM.Application.Contracts.ArticleCategory;
using BM.Domain.ArticleAgg;
using BM.Domain.ArticleCategoryAgg;
using BM.Infrastructure.EfCore;
using BM.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BM.Infrastructure.Configuration
{
    public class BlogManagementrBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();

            services.AddTransient<IArticleApplication, ArticleApplication>();
            services.AddTransient<IArticleRepository,  ArticleRepository>();

            services.AddDbContext<BlogContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
