﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.CommentAgg;

namespace SM.Infrastructure.EfCore.Mapping
{
    public class CommentMapping:IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Email).HasMaxLength(300);
            builder.Property(x => x.Message).HasMaxLength(1000);

            builder.HasOne(x => x.Product)
                   .WithMany(x => x.Comments) 
                   .HasForeignKey(x => x.ProductId);
        }
    }
}