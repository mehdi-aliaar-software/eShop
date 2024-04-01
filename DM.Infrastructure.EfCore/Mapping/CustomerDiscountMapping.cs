using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DM.Infrastructure.EfCore.Mapping
{
    public class CustomerDiscountMapping:IEntityTypeConfiguration<CustomerDiscount>
    {
        public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
        {
            builder.ToTable("CustomerDiscounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Reason).HasMaxLength(500);
        }
    }
}
