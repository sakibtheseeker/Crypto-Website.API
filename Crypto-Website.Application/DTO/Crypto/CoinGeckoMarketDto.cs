using System.Text.Json.Serialization;

namespace Crypto_Website.Application.DTO.Crypto
{
    public class CoinGeckoMarketDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("image")]
        public string Symbol { get; set; } = null!;

        [JsonPropertyName("current_price")]
        public decimal CurrentPrice { get; set; }
    }
}