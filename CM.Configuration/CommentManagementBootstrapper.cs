using CM.Application;
using CM.Application.Contracts.Comment;
using CM.Domain.CommentAgg;
using CM.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SM.Infrastructure.EfCore.Repository;

namespace SM.Configuration
{
    public static class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
 
            services.AddTransient<ICommentApplication, CommentApplication>();
            services.AddTransient<ICommentRepository,  CommentRepository>();

            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));

        }

    }
}
