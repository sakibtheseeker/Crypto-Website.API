using Crypto_Website.Application.DTO.Portfolio;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Crypto_Website.Infrastructure.Repository
{
    public class PortfolioAssetRepository : IPortfolioAssetRepository
    {
        private readonly ApplicationDBContext _db;

        public PortfolioAssetRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<PortfolioAsset?> GetAsync(int pid, int cid)
        {
            return await _db.PortfolioAssets
                .FirstOrDefaultAsync(a =>
                    a.Pid == pid &&
                    a.Cid == cid &&
                    a.DeletedAt == null);
        }

        public async Task<List<PortfolioAssetResponseDto>> GetByPortfolioIdAsync(int pid)
        {
            return await (
                from a in _db.PortfolioAssets
                join c in _db.Cryptos on a.Cid equals c.Cid
                where a.Pid == pid && a.DeletedAt == null
                select new PortfolioAssetResponseDto
                {
                    Paid = a.Paid,
                    Cid = a.Cid,
                    CryptoName = c.Cname,
                    Symbol=c.Csymbol,
                    Image=c.CimageUrl,
                    Quantity = a.Quantity,
                    AvgBuyPrice = a.AvgBuyPrice,
                    Market_cap=a.Market_cap,
                    Total_volume=a.Total_volume,
                    Price_change_24h=a.Price_change_24h,
                    Price_change_percentage_24h= a.Price_change_percentage_24h
                }
            ).ToListAsync();
        }

        public async Task AddAsync(PortfolioAsset asset)
        {
            _db.PortfolioAssets.Add(asset);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(PortfolioAsset asset)
        {
            //_db.PortfolioAssets.Update(asset);
            if (asset.Quantity == 0)
            {
                _db.PortfolioAssets.Remove(asset);
            }
            else
            {
                _db.PortfolioAssets.Update(asset);
            }
            await _db.SaveChangesAsync();
        }
    }
}
