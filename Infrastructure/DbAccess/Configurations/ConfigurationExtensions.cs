using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Problems.Warehouse.Domain;

namespace Problems.Warehouse.Infrastructure.DbAccess.Configurations
{
    public static class ConfigurationExtensions
    {
        public static EntityTypeBuilder<TEntity> HasId<TEntity, TId>(
            this EntityTypeBuilder<TEntity> entityTypeBuilder)
            where TEntity : BaseEntity<TId>
            where TId : struct, IEquatable<TId>
        {
            entityTypeBuilder
                .HasKey("_underlyingId");

            entityTypeBuilder
                .Property("_underlyingId").HasColumnName("Id");

            return entityTypeBuilder;
        }
    }
}