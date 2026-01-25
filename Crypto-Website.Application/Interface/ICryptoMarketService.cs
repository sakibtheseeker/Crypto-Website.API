using Crypto_Website.Application.DTO.Crypto;

namespace Crypto_Website.Application.Interface
{
    public interface ICryptoMarketService
    {
        Task<List<CoinGeckoMarketDto>> GetMarketCryptosAsync();
    }
}
