using System.ComponentModel.DataAnnotations;

namespace Crypto_Website.Domain.Models
{
    public class PortfolioAsset
    {
        [Key]
        public int Paid { get; private set; }
        public int Pid { get; private set; }
        public int Cid { get; private set; }

        public decimal Quantity { get; private set; }
        public decimal AvgBuyPrice { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private PortfolioAsset() { } // EF Core

        public PortfolioAsset(int pid, int cid, decimal quantity, decimal price)
        {
            Pid = pid;
            Cid = cid;
            Quantity = quantity;
            AvgBuyPrice = price;
            CreatedAt = DateTime.UtcNow;
        }

        public void Buy(decimal quantity, decimal price)
        {
            if (quantity <= 0) return;

            var totalCost =
                (Quantity * AvgBuyPrice) +
                (quantity * price);

            Quantity += quantity;
            AvgBuyPrice = totalCost / Quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Sell(decimal quantity)
        {
            if (quantity <= 0 || Quantity < quantity) return;

            Quantity -= quantity;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
