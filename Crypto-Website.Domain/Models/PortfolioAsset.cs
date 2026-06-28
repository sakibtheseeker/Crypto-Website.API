using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Domain.Models
{
    public class PortfolioAsset
    {
        [Key]
        public int PortfolioAssetId { get; set; }
        [ForeignKey("Portfolio")]
        public int PortfolioId { get; set; }
        [ForeignKey("Crypto")]
        public int CryptoId { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal AvgBuyPrice { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Portfolio Portfolio { get; set; }
        public Crypto Crypto { get; set; }
    }



    
}
