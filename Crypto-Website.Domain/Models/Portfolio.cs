using System.ComponentModel.DataAnnotations;

namespace Crypto_Website.Domain.Models
{
    public class Portfolio
    {
        [Key]
        public int Pid { get; set; }
        public int Uid { get; set; }
        public int Cid { get; set; }

        public decimal Quantity { get; set; }
        public decimal AvgBuyPrice { get; set; }

        public bool IsActive { get; set; } = true;

        public void Buy(decimal quantity, decimal price)
        {
            var totalCost = (Quantity * AvgBuyPrice) + (quantity * price);
            Quantity += quantity;
            AvgBuyPrice = totalCost / Quantity;
        }

        public void Sell(decimal quantity)
        {
            Quantity -= quantity;
        }
    }
}
