using System;

namespace Problems.Warehouse.Domain
{
    public class BaseEntity<TId> : IEntity<TId>
        where TId : struct, IEquatable<TId>
    {
        public TId Id { get; private set; }

#pragma warning disable 8618
        // to satisfy ef core
        protected BaseEntity()
        {
        }
#pragma warning restore 8618

        // protected BaseEntity(TId id)
        // {
        //     _underlyingId = id.Value;
        //     Id = id;
        // }
    }
}