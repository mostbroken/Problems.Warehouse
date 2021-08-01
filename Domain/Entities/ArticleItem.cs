using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Problems.Warehouse.Domain.Entities
{
    public class ArticleItem : BaseEntity<int> //, IEquatable<ArticleItem>
    {
#pragma warning disable 8618
        // to satisfy ef core
        private ArticleItem()
#pragma warning restore 8618
        {
        }

        public ArticleItem(ArticleId articleId)
        {
            ArticleId = articleId.Value;
        }

        public int ArticleId { get; private set; }

        public Article Article { get; private set; }

        public Warehouse Warehouse { get; private set; }

        public int Quantity { get; private set; }

        public void IncrementQuantity()
        {
            Quantity++;
        }
        
        public void DecrementQuantity()
        {
            if (Quantity <= 0)
                throw new InvalidOperationException($"Is empty already");
            
            Quantity--;
        }
    }
}