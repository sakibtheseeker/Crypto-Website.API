using Crypto_Website.Application.DTO;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Crypto_Website.Infrastructure.Repository
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly ApplicationDBContext db;

        public CryptoRepository(ApplicationDBContext db)
        {
            this.db = db;
        }

        public async Task<List<Crypto>> GetAllAsync()
        {
            return await db.Cryptos
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(string symbol)
        {
            return await db.Cryptos
                .AnyAsync(x => x.Csymbol == symbol && x.IsActive);
        }


        public async Task AddAsync(Crypto crypto)
        {
            db.Cryptos.Add(crypto);
            await db.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(int cid)
        {
            return await db.Cryptos.AnyAsync(c => c.Cid == cid);
        }

        public async Task<Crypto> GetByIdAsync(int cid)
        {
            return await db.Cryptos
                .FirstAsync(c => c.Cid == cid && c.IsActive);
        }

        public async Task<(List<Crypto> Data, int TotalRecords)>GetPagedAsync(int pageNumber, int pageSize)
        {
          
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = db.Cryptos
                .Where(c => c.IsActive);

            var totalRecords = await query.CountAsync();

            var data = await query
                 .OrderBy(c => c.Cid)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalRecords);
        }

        public async Task<Crypto?> GetBySymbolAsync(string symbol)
        {
            return await db.Cryptos
                .FirstOrDefaultAsync(x => x.Csymbol == symbol && x.IsActive);
        }

        public async Task UpdateAsync(Crypto crypto)
        {
            db.Cryptos.Update(crypto);
            await db.SaveChangesAsync();
        }

        public async Task<List<UserHoldingDTO>> GetUserPortfolioAssetsAsync(int uid)
        {
            return await (
                from p in db.Portfolios
                join pa in db.PortfolioAssets on p.Pid equals pa.Pid
                where p.Uid == uid
                      && p.IsActive
                      && p.DeletedAt == null
                select new UserHoldingDTO
                {
                    Cid = pa.Cid,
                    Quantity = pa.Quantity
                }
            )
            .AsNoTracking()
            .ToListAsync();
        }








    }
}
