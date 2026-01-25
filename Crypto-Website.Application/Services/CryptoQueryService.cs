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
                Price = c.CurrentPrice,
                Provider = c.ProviderName
            }).ToList();
        }
    }
}
