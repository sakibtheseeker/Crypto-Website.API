using System.Text.Json.Serialization;

namespace Crypto_Website.Application.DTO.Crypto
{
    public class CoinGeckoMarketDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = null!;

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; } = null!;


        [JsonPropertyName("current_price")]
        public decimal CurrentPrice { get; set; }

        [JsonPropertyName("market_cap")]
        public decimal? Market_cap { get; set; }

        [JsonPropertyName("total_volume")]
        public decimal? Total_volume { get; set; }

        [JsonPropertyName("price_change_24h")]
        public decimal? Price_change_24h { get; set; }

        [JsonPropertyName("price_change_percentage_24h")]
        public decimal? Price_change_percentage_24h { get; set; }
    }
}