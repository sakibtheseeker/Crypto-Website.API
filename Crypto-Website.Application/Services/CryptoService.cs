using Crypto_Website.Application.DTO.Crypto;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Application.Helper;

namespace Crypto_Website.Application.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly ICryptoRepository _cryptoRepo;
        private readonly ICryptoMarketService _marketService;

        public CryptoService(
            ICryptoRepository cryptoRepo,
            ICryptoMarketService marketService)
        {
            _cryptoRepo = cryptoRepo;
            _marketService = marketService;
        }

        public async Task<List<CryptoResponseDto>> GetCryptosAsync()
        {
            var cryptos = await _cryptoRepo.GetAllAsync();

            return cryptos.Select(x => new CryptoResponseDto
            {
                Id = x.Cid,
                Name = x.Cname,
                Symbol = x.Csymbol,
                Price = x.CurrentPrice,
                Provider = x.ProviderName
            }).ToList();
        }

        public async Task<object> GetCryptosAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var (data, totalRecords) =
                await _cryptoRepo.GetPagedAsync(pageNumber, pageSize);

            var result= data.Select(x => new CryptoResponseDto
            {
                Id = x.Cid,
                Name = x.Cname,
                Symbol = x.Csymbol,
                Price = x.CurrentPrice,
                Provider = x.ProviderName
            }).ToList();

            return new PagedResponse< CryptoResponseDto >(result, pageNumber, pageSize, totalRecords, totalRecords);
        }

        public async Task<int> SyncCryptosFromMarketAsync()
        {
            var coins = await _marketService.GetMarketCryptosAsync();
            int inserted = 0;

            foreach (var coin in coins)
            {
                if (await _cryptoRepo.ExistsAsync(coin.Symbol.ToUpper()))
                    continue;

                var crypto = new Crypto
                {
                    Cname = coin.Name,
                    Csymbol = coin.Symbol,
                    CurrentPrice = coin.CurrentPrice,
                    ProviderName = "coingecko",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _cryptoRepo.AddAsync(crypto);
                inserted++;
            }

            return inserted;
        }


        public async Task<ApiResponse<CryptoResponseDto>> GetCryptosByIdAsync(int cid)
        {
            var cry = await _cryptoRepo.GetByIdAsync(cid);

            if(cry==null)
            {
                throw new KeyNotFoundException($"Crypto with ID {cid} not found");
            }

            var result= new CryptoResponseDto
            {
                Id=cry.Cid,
                Name = cry.Cname,
                Symbol = cry.Csymbol,
                Price = cry.CurrentPrice,
                Provider = cry.ProviderName
            };

            return ApiResponse<CryptoResponseDto>.SuccessResponse(result, "Crypto Fetched Successfully");
        }
    }
}
