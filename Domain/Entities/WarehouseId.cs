using System;

namespace Problems.Warehouse.Domain.Entities
{
    public readonly struct WarehouseId : IId<int>, IEquatable<WarehouseId>
    {
        public WarehouseId(int value)
        {
            if (value <= 0) 
                throw new ArgumentOutOfRangeException(nameof(value));
            
            Value = value;
        }

        public int Value { get; }

        #region Equatable

        public bool Equals(WarehouseId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            return obj is WarehouseId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(WarehouseId left, WarehouseId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WarehouseId left, WarehouseId right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}