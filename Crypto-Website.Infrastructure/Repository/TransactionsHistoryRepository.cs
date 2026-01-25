using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class TransactionsHistoryRepository : ITransactionsHistoryRepository
{
    private readonly ApplicationDBContext _db;

    public TransactionsHistoryRepository(ApplicationDBContext db)
    {
        _db = db;
    }

    public async Task AddAsync(TransactionsHistory history)
    {
        _db.TransactionsHistories.Add(history);
        await _db.SaveChangesAsync();
    }

    public async Task<List<TransactionsHistory>> GetByUserIdAsync(int uid)
    {
        return await _db.TransactionsHistories
            .Where(t => t.Uid == uid && t.IsActive)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

}
