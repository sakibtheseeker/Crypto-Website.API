using AutoMapper;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


public class WalletTransactionRepository : IWalletTransactionRepository
{
    private readonly ApplicationDBContext _db;


    public WalletTransactionRepository(ApplicationDBContext db)
    { 
        _db = db;
    }

    public async Task<List<WalletTransaction>> GetByWalletIdAsync(int wid)
    {
        return await _db.WalletTransactions
            .Where(t => t.Wid == wid && t.IsActive)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(WalletTransaction tx)
    {
        _db.WalletTransactions.Add(tx);
        await _db.SaveChangesAsync();
    }
}
