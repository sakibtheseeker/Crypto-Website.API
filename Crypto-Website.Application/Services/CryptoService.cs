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

        //public async Task<ApiResponse<List<CryptoResponseDto>>> GetCryptosAsync()
        //{
        //    var cryptos = await _cryptoRepo.GetAllAsync();

        //    var result= cryptos.Select(x => new CryptoResponseDto
        //    {
        //        Id = x.Cid,
        //        Name = x.Cname,
        //        Symbol = x.Csymbol,
        //        Price = x.CurrentPrice,
        //        Provider = x.ProviderName
        //    }).ToList();

        //    return ApiResponse<List<CryptoResponseDto>>.SuccessResponse(result,"Crypto Inserted Successfully");
        //}

        public async Task<object> GetCryptosAsync(int uid,int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var pagedResult = await _cryptoRepo.GetPagedAsync(pageNumber, pageSize);
            var data = pagedResult.Data;
            var totalRecords = pagedResult.TotalRecords;

            var portfolioAssets = await _cryptoRepo.GetUserPortfolioAssetsAsync(uid);

            var holdingsMap = portfolioAssets.ToDictionary(
                x => x.Cid,
                x => x.Quantity
            );



            var result = data.Select(x => new CryptoResponseDto
            {
                Id = x.Cid,
                Name = x.Cname,
                Symbol = x.Csymbol,
                Image=x.CimageUrl,
                Price = x.CurrentPrice,
                Provider = x.ProviderName,
                Market_cap=x.market_cap,
        
                Total_volume=x.total_volume,
                Price_change_24h=x.price_change_24h,
                Price_change_percentage_24h=x.price_change_percentage_24h,
                Quantity = holdingsMap.ContainsKey(x.Cid)
        ? holdingsMap[x.Cid]
        : 0   
            }).ToList();

            return new PagedResponse< CryptoResponseDto >(result, pageNumber, pageSize, totalRecords, totalRecords);
        }

        public async Task<ApiResponse<int>> SyncCryptosFromMarketAsync()
        {
            var coins = await _marketService.GetMarketCryptosAsync();
            int affected = 0;

            foreach (var coin in coins)
            {
                var symbol = coin.Symbol.ToUpper();
                var crypto = await _cryptoRepo.GetBySymbolAsync(symbol);

                if (crypto != null)
                {
                   
                    crypto.CurrentPrice = coin.CurrentPrice;
                    crypto.market_cap = coin.Market_cap;
                    crypto.total_volume = coin.Total_volume;
                    crypto.price_change_24h = coin.Price_change_24h;
                    crypto.price_change_percentage_24h = coin.Price_change_percentage_24h;
                    crypto.CimageUrl = coin.ImageUrl;
                    crypto.UpdatedAt = DateTime.UtcNow;

                    await _cryptoRepo.UpdateAsync(crypto);
                }
                else
                {
                  
                    await _cryptoRepo.AddAsync(new Crypto
                    {
                        Cname = coin.Name,
                        Csymbol = symbol,
                        CimageUrl = coin.ImageUrl,
                        CurrentPrice = coin.CurrentPrice,
                        ProviderName = "coingecko",
                        market_cap = coin.Market_cap,
                        total_volume = coin.Total_volume,
                        price_change_24h = coin.Price_change_24h,
                        price_change_percentage_24h = coin.Price_change_percentage_24h,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    });
                }

                affected++;
            }

            return ApiResponse<int>.SuccessResponse(
                affected,
                "Crypto prices synced successfully"
            );
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
                Image=cry.CimageUrl,

                Price = cry.CurrentPrice,
                Provider = cry.ProviderName,

                Market_cap=cry.market_cap,
                Total_volume=cry.total_volume,
                Price_change_24h=cry.price_change_24h,
                Price_change_percentage_24h=cry.price_change_percentage_24h
            };

            return ApiResponse<CryptoResponseDto>.SuccessResponse(result, "Crypto Fetched Successfully");
        }
    }
}
