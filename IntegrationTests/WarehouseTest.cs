using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Problems.Warehouse.Api;
using Problems.Warehouse.Api.WarehouseEndpoints;
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
        public async Task GetWarehouses()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Warehouse");
            var payload = await response.Content.ReadAsStreamAsync();

            var warehouses = await JsonSerializer.DeserializeAsync<WarehouseDto[]>(
                payload,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));

            var expected = Enumerable
                .Range(1, 3)
                .Select(x => new WarehouseDto (x,  $"address{x}"))
                .ToArray();

            Assert.Equal(expected, warehouses);
        }
    }
}


