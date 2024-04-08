using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BM.Infrastructure.EfCore.Mappings
{
    public class ArticleMapping:IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");

            builder.Property(x => x.Title).HasMaxLength(500);
            builder.Property(x => x.ShortDescription).HasMaxLength(2000);
            builder.Property(x => x.PictureAlt).HasMaxLength(200);
            builder.Property(x => x.PictureTitle).HasMaxLength(200);
            builder.Property(x => x.Picture).HasMaxLength(500);
            builder.Property(x => x.Slug).HasMaxLength(600);
            builder.Property(x => x.Keywords).HasMaxLength(100);
            builder.Property(x => x.MetaDescription).HasMaxLength(150);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(1000);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Articles)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
