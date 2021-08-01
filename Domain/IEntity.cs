using System;

namespace Problems.Warehouse.Domain
{
    public interface IEntity<out TId>
        where TId : struct, IEquatable<TId>
    {
        TId Id { get; }
    }
}