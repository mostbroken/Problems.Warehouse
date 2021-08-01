using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Problems.Warehouse.Domain.Entities;

namespace Problems.Warehouse.Infrastructure.DbAccess.Configurations
{
    public class ArticleItemConfiguration : IEntityTypeConfiguration<ArticleItem>
    {
        public void Configure(EntityTypeBuilder<ArticleItem> builder)
        {
           // builder.HasId<ArticleItem, int>();
            
            // builder.HasOne(pi => pi.Article);
            // builder.HasOne(pi => pi.Warehouse)
            //     .WithMany(w => w.Articles)
            //     .HasForeignKey(pi => pi.ArticleId);
            
        }
    }
}