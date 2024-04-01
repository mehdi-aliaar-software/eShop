using DM.Domain.ColleagueDiscountAgg;
using DM.Domain.CustomerDiscountAgg;
using DM.Infrastructure.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DM.Infrastructure.EfCore
{
    public class DiscountContext : DbContext
    {
        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }
        public DbSet<ColleagueDiscount> ColleagueDiscounts { get; set; }

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly=typeof(CustomerDiscountMapping).Assembly  ;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
