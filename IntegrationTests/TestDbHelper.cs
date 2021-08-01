using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Problems.Warehouse.Api;
using Problems.Warehouse.Commands;
using Problems.Warehouse.Infrastructure.DbAccess;

namespace Problems.Warehouse.IntegrationTests
{
    public static class TestDbHelper
    {
        public static async Task ResetDb(WebApplicationFactory<Startup> factory)
        {
            using var scope = factory.Services.CreateScope();
            
            var dbContext = scope.ServiceProvider.GetService<WarehouseDbContext>();
            await dbContext!.Database.EnsureDeletedAsync();
            await dbContext!.Database.EnsureCreatedAsync();
        }
        
        public static async Task<IReadOnlyList<int>> CreateWarehouses(
            WebApplicationFactory<Startup> factory, 
            IEnumerable<string> addresses)
        {
            using var scope = factory.Services.CreateScope();
            
            var dbContext = scope.ServiceProvider.GetService<WarehouseDbContext>();

            var newWarehouses = addresses.Select(a => new Domain.Entities.Warehouse(a)).ToArray();
            
            foreach (var warehouse in newWarehouses)
            {
                dbContext.Warehouses.Add(warehouse);
            }

            await dbContext.SaveChangesAsync();

            return newWarehouses.Select(w => w.Id).ToArray();
        }

    }
}