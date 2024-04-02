using IM.Domain.InventoryAgg;
using IM.Infrastructure.EfCore.Mappings;
using Microsoft.EntityFrameworkCore;

namespace IM.Infrastructure.EfCore
{
    public class InventoryContext:DbContext
    {
        public DbSet<Inventory> Inventory { get; set; }
        public InventoryContext(DbContextOptions<InventoryContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(InventoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
