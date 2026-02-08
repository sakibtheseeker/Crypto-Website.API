using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


public class PortfolioRepository : IPortfolioRepository
{
    private readonly ApplicationDBContext _db;

    public PortfolioRepository(ApplicationDBContext db)
    {
        _db = db;
    }

    public async Task<Portfolio?> GetByUserIdAsync(int uid)
    {
        return await _db.Portfolios
            .Include(p => p.Assets)
            .FirstOrDefaultAsync(p => p.Uid == uid &&p.IsActive == true && p.DeletedAt == null);

    }


    public async Task AddAsync(Portfolio portfolio)
    {
        _db.Portfolios.Add(portfolio);
        await _db.SaveChangesAsync();
    }
}
