using Crypto_Website.Application.DTO.Crypto;
using Crypto_Website.Application.Interface;

namespace Crypto_Website.Application.Services
{
    public class CryptoQueryService
    {
        private readonly ICryptoRepository _cryptoRepository;

        public CryptoQueryService(ICryptoRepository cryptoRepository)
        {
            _cryptoRepository = cryptoRepository;
        }

        public async Task<List<CryptoResponseDto>> GetCryptosAsync()
        {
            var cryptos = await _cryptoRepository.GetAllAsync();

            return cryptos.Select(c => new CryptoResponseDto
            {
                Id = c.Cid,
                Name = c.Cname,
                Symbol = c.Csymbol,
                Image = c.CimageUrl,
                Price = c.CurrentPrice,
                Provider = c.ProviderName,
                Market_cap=c.market_cap,
                Total_volume=c.total_volume,
                Price_change_24h=c.price_change_24h,
                Price_change_percentage_24h=c.price_change_percentage_24h


            }).ToList();
        }
    }
}
