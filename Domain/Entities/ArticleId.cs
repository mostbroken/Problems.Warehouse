using System;

namespace Problems.Warehouse.Domain.Entities
{
    public readonly struct ArticleId : IId<int>, IEquatable<ArticleId>
    {
        public ArticleId(int value)
        {
            if (value <= 0) 
                throw new ArgumentOutOfRangeException(nameof(value));

            Value = value;
        }

        public  int Value { get; }
        
        #region Equality

        public bool Equals(ArticleId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            return obj is ArticleId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(ArticleId left, ArticleId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ArticleId left, ArticleId right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}