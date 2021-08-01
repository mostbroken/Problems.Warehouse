using System;

namespace Problems.Warehouse.Domain.Entities
{
    public class Article : BaseEntity<int>, IAggregateRoot, IEquatable<Article> //, ISoftDeletable
    {
#pragma warning disable 8618
        // to satisfy ef core
        private Article()
#pragma warning restore 8618
        {
        }

        public Article(string name, string? description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }

        public string? Description { get; private set; }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(nameof(newName)))
                throw new ArgumentException($"{nameof(newName)} is empty");

            Name = newName;
        }

        public void UpdateDescription(string newDescription)
        {
            Description = string.IsNullOrWhiteSpace(newDescription) ? null: newDescription;
        }

        #region Equality

        public bool Equals(Article? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Id.Equals(other.Id);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Article) obj);
        }

        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}