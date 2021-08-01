using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Problems.Warehouse.Infrastructure.DbAccess.Configurations
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Domain.Entities.Warehouse>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Warehouse> builder)
        {
            builder
                .Navigation(e => e.Articles).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}