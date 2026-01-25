using Crypto_Website.Application.DTO.Portfolio;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Crypto_Website.Infrastructure.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _db;

        public PortfolioRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<Portfolio?> GetAsync(int uid, int cid)
        {
            return await _db.Portfolios
                .FirstOrDefaultAsync(p =>
                    p.Uid == uid &&
                    p.Cid == cid &&
                    p.IsActive);
        }

        public async Task<List<PortfolioResponseDto>> GetByUserIdAsync(int uid)
        {
            return await (
                from p in _db.Portfolios
                join c in _db.Cryptos on p.Cid equals c.Cid
                where p.Uid == uid && p.IsActive
                select new PortfolioResponseDto
                {
                    Pid = p.Pid,
                    CryptoName = c.Cname,
                    Quantity = p.Quantity,
                    AvgBuyPrice = p.AvgBuyPrice
                }
            ).ToListAsync();
        }

        public async Task AddAsync(Portfolio portfolio)
        {
            _db.Portfolios.Add(portfolio);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Portfolio portfolio)
        {
            _db.Portfolios.Update(portfolio);
            await _db.SaveChangesAsync();
        }
    }
}
