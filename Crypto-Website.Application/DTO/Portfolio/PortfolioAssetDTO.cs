using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Portfolio
{
    public class PortfolioAssetDTO
    {
        public int PortfolioAssetId { get; set; }
        public int PortfolioId { get; set; }
        public int CryptoId { get; set; }
        public string CryptoName { get; set; }
        public decimal Quantity { get; set; }
        public decimal AvgBuyPrice { get; set; }
        public string CryptoSymbol { get; set; }
        public string CryptoImage { get; set; }
        public decimal CurrentPrice { get; set; }
        public string ProviderName { get; set; }
        public decimal? MarketCap { get; set; }
        public decimal? TotalVolume { get; set; }
        public decimal? PriceChange24h { get; set; }
        public decimal? PriceChangepercentage24h { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
