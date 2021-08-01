using System;
using System.Linq;
using Problems.Warehouse.Domain.Entities;

namespace Problems.Warehouse.Infrastructure.DbAccess
{
    public static class DbInitializer
    {
        public static void Initialize(WarehouseDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Warehouses.Any())
            {
                return; // DB has been seeded
            }

            var warehouses = new Domain.Entities.Warehouse[]
            {
                new Domain.Entities.Warehouse("address 1"),
                new Domain.Entities.Warehouse("address 2"),
                new Domain.Entities.Warehouse("address 3"),
            };
            
            var articles = new Article[]
            {
                new Article("Article 1", "Article 1 description"),
                new Article("Article 2", "Article 2 description"),
                new Article("Article 3", "Article 3 description"),
            };

            context.Warehouses.AddRange(warehouses);
            context.Articles.AddRange(articles);
            context.SaveChanges();

            warehouses[0].IncrementArticleQuantity(new ArticleId(articles[1].Id));
            warehouses[1].IncrementArticleQuantity(new ArticleId(articles[2].Id));
            context.SaveChanges();
        }
    }
}