using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Crypto_Website.Infrastructure.Repository
{
    public class PortfolioAssetRepository : IPortfolioAssetRepository
    {
        private readonly ApplicationDBContext _context;

        public PortfolioAssetRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<PortfolioAsset?> GetAsync(int pid, int cid)
        {
            return await _context.PortfolioAssets
                .FirstOrDefaultAsync(x =>
                    x.Pid == pid &&
                    x.Cid == cid);
        }

        public async Task AddAsync(PortfolioAsset asset)
        {
            _context.PortfolioAssets.Add(asset);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PortfolioAsset asset)
        {
            _context.PortfolioAssets.Update(asset);
            await _context.SaveChangesAsync();
        }
    }
}
