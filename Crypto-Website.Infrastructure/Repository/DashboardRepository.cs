using Crypto_Website.Application.DTO.Dashboard;
using Crypto_Website.Application.Interface;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Infrastructure.Repository
{
    public class DashboardRepository :IDashboardRepository
    {
        private readonly ApplicationDBContext _db;

        public DashboardRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<decimal> GetWalletBalanceAsync(int uid)
        {
            return await _db.Wallets
                .Where(w => w.Uid == uid && w.IsActive)
                .Select(w => w.CurrentBal)
                .FirstOrDefaultAsync();
        }

        public async Task<List<DashboardAssetDTO>> GetPortfolioAssetsAsync(int uid)
        {
            return await (
                from p in _db.Portfolios
                join pa in _db.PortfolioAssets on p.Pid equals pa.Pid
                join c in _db.Cryptos on pa.Cid equals c.Cid
                where p.Uid == uid
                      && p.IsActive
                      && pa.DeletedAt == null
                select new DashboardAssetDTO
                {
                    Cid = c.Cid,
                    CryptoName = c.Cname,
                    Symbol = c.Csymbol,
                    Quantity = pa.Quantity,
                    CurrentPrice = c.CurrentPrice,
                    BuyPrice = pa.  AvgBuyPrice,
                    Image = c.CimageUrl
                }
            ).ToListAsync();
        }
    }
}
