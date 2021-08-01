using Microsoft.EntityFrameworkCore;
using Problems.Warehouse.Domain.Entities;
using Problems.Warehouse.Infrastructure.DbAccess.Configurations;

namespace Problems.Warehouse.Infrastructure.DbAccess
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Domain.Entities.Warehouse> Warehouses { get; set; }
        
        public DbSet<Article> Articles { get; set; }
        
        public DbSet<ArticleItem> ArticleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleItemConfiguration());
        }
    }
}