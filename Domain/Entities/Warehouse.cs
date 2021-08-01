using System;
using System.Collections.Generic;
using System.Linq;

namespace Problems.Warehouse.Domain.Entities
{
    public class Warehouse : BaseEntity<int>, IAggregateRoot, IEquatable<Warehouse> //, ISoftDeletable
    {
        private readonly List<ArticleItem> _articles = new List<ArticleItem>();

#pragma warning disable 8618
        // to satisfy ef core
        private Warehouse()
#pragma warning restore 8618
        {
        }

        public Warehouse(string address)
        {
            Address = address;
        }

        public IReadOnlyCollection<ArticleItem> Articles => _articles.AsReadOnly();
        public string Address { get; private set; }

        public void UpdateAddress(string newAddress)
        {
            if (string.IsNullOrWhiteSpace(nameof(newAddress)))
                throw new ArgumentException($"{nameof(newAddress)} is empty");

            Address = newAddress;
        }

        public void DecrementArticleQuantity(ArticleId articleId)
        {
            var existed = _articles.SingleOrDefault(item => item.ArticleId == articleId.Value);
            if (existed == null) return;
            
            existed.DecrementQuantity();

            if (existed.Quantity == 0)
                _articles.Remove(existed);
        }
        
        public void IncrementArticleQuantity(ArticleId articleId)
        {
            var articleItem = _articles.SingleOrDefault(item => item.ArticleId == articleId.Value);
            if (articleItem == null)
            {
                articleItem = new ArticleItem(articleId);
                _articles.Add(articleItem);
            }

            articleItem.IncrementQuantity();
        }

        #region Equatable

        public bool Equals(Warehouse? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Warehouse) obj);
        }

        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}