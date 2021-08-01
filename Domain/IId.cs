using System;

namespace Problems.Warehouse.Domain
{
    public interface IId<out TUnderlying> where TUnderlying : struct, IEquatable<TUnderlying>
    {
        TUnderlying Value { get; }
    }
}