using Crypto_Website.Application.DTO.Crypto;
using Crypto_Website.Application.Interface;
using System.Net.Http.Json;

namespace Crypto_Website.Application.Services
{
    public class CryptoMarketService : ICryptoMarketService
    {
        private readonly HttpClient _httpClient;

        public CryptoMarketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CoinGeckoMarketDto>> GetMarketCryptosAsync()
        {
            var response = await _httpClient.GetAsync(
                "api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=123&page=1"
            );

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<List<CoinGeckoMarketDto>>() ?? new();
        }
    }
}
