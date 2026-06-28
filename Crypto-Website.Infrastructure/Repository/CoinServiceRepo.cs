using AutoMapper;
using Crypto_Website.Application.DTO;
using Crypto_Website.Application.Interface;
using Crypto_Website.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Infrastructure.Repository
{
    public class CoinServiceRepo : ICoinService
    {
        ICrypto Crypto;
        IGeckoCoinservice Gecko;
        IMapper _mapper;
        ApplicationDbContext _context;
        public CoinServiceRepo(ICrypto crypto, IGeckoCoinservice gecko,IMapper mapper,ApplicationDbContext _context) 
        {
            Crypto = crypto;
            Gecko = gecko;  
            _mapper = mapper;
            this._context = _context;
        }
        public async Task FetchAndSave()
        {
            var Coins = await Gecko.GetCoins();
            var crypto = _context.Cryptos.ToList();

            foreach (var coin in Coins)
            {
                var existingCrypto = crypto.FirstOrDefault(x =>
                x.CryptoSymbol == coin.symbol &&
                x.ProviderName == "GeckoApi");
                if (existingCrypto != null)
                {
                    existingCrypto.CurrentPrice = coin.current_price;
                    existingCrypto.MarketCap = coin.market_cap;
                    existingCrypto.TotalVolume = coin.total_volume;
                    existingCrypto.PriceChange24h = coin.price_change_24h;
                    existingCrypto.PriceChangepercentage24h = coin.price_change_percentage_24h;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var c = new CryptoRequestDto
                    {
                        CryptoName = coin.name,
                        CryptoSymbol = coin.symbol,
                        CryptoImage = coin.image,
                        CurrentPrice = coin.current_price,
                        ProviderName = "GeckoApi",
                        MarketCap = coin.market_cap,
                        TotalVolume = coin.total_volume,
                        PriceChange24h = coin.price_change_24h,
                        PriceChangepercentage24h = coin.price_change_percentage_24h,

                        IsActive = true,

                    };
                    Crypto.Add(c);
                    
                    
                }
                
            }
        }
    }
}


