using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Problems.Warehouse.Domain.Entities;

namespace Problems.Warehouse.Infrastructure.DbAccess.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
           // builder.HasId<Article, int>();
        }
    }
}