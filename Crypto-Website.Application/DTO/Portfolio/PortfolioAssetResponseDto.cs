namespace Crypto_Website.Application.DTO.Portfolio
{
    public class PortfolioAssetResponseDto
    {
        public int Paid { get; set; }
        public int Cid { get; set; }
        public string CryptoName { get; set; }
        public decimal Quantity { get; set; }
        public string Symbol { get; set; } = null!;
        public string Image { get; set; } = null!;
        public decimal? Market_cap { get; set; }
        public decimal? Total_volume { get; set; }
        public decimal? Price_change_24h { get; set; }
        public decimal? Price_change_percentage_24h { get; set; }
        public decimal AvgBuyPrice { get; set; }
    }
}
