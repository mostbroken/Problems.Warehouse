using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Problems.Warehouse.Api;
using Problems.Warehouse.Commands;
using Problems.Warehouse.Domain.Entities;
using Problems.Warehouse.Infrastructure.DbAccess;
using Xunit;

namespace Problems.Warehouse.IntegrationTests
{
    public class WarehouseTest : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        private readonly WebApplicationFactory<Api.Startup> _factory;

        public WarehouseTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateWarehousesTest()
        {
            // Arrange
            await TestDbHelper.ResetDb(_factory);

            var client = _factory.CreateClient();
            
            var warehousesToSend = Enumerable
                .Range(1, 3)
                .Select(x => $"Address{x}")
                .ToArray();

            // Act
            await SendCreateWarehousesRequest(warehousesToSend, client);

            var serverWarehouses = await ServerWarehouses(client);

            // Assert
            Assert.Equal(warehousesToSend, serverWarehouses.Select(w => w.Address).OrderBy(x => x));
            
            static async Task SendCreateWarehousesRequest(string[] strings, HttpClient httpClient)
            {
                await Task.WhenAll(strings
                    .Select(x => httpClient.PostAsync($"api/Warehouse?address={x}", new StreamContent(new MemoryStream())))
                    .ToList());
            }
        }
        
        
        [Fact]
        public async Task DeleteWarehousesTest()
        {
            // Arrange
            await TestDbHelper.ResetDb(_factory);

            var client = _factory.CreateClient();
            
            var newAddresses = Enumerable
                .Range(1, 3)
                .Select(x => $"Address{x}")
                .ToArray()!;

            var newWarehousesIds = await TestDbHelper.CreateWarehouses(_factory, newAddresses);

            // Act
            await SendDeleteWarehousesRequest(newWarehousesIds[0], client);
            
            // Assert
            var serverWarehouses = await ServerWarehouses(client);

            Assert.Equal(newAddresses.Skip(1), serverWarehouses.Select(w => w.Address).OrderBy(x => x));
            
            static async Task SendDeleteWarehousesRequest(int id, HttpClient httpClient) => await httpClient.DeleteAsync($"api/Warehouse?id={id}");
        }
        
        private static async Task<WarehouseItem[]> ServerWarehouses(HttpClient httpClient)
        {
            var warehousesFromServer = await httpClient.GetAsync("/api/Warehouse");
            var payload = await warehousesFromServer.Content.ReadAsStreamAsync();

            var serverWarehouses = await JsonSerializer.DeserializeAsync<WarehouseItem[]>(
                payload,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
                
            return serverWarehouses!;
        }

    }
}