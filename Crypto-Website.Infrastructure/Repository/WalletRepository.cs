using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class WalletRepository : IWalletRepository
{
    private readonly ApplicationDBContext _db;

    public WalletRepository(ApplicationDBContext db)
    {
        _db = db;
    }

    public async Task<Wallet?> GetByUserIdAsync(int uid)
    {
        return await _db.Wallets
            .FirstOrDefaultAsync(w => w.Uid == uid && w.IsActive);
    }
    public async Task AddAsync(Wallet wallet)
    {
        _db.Wallets.Add(wallet);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Wallet wallet)
    {
        _db.Wallets.Update(wallet);
        //db.WalletTransactions.Update(wallet);
        await _db.SaveChangesAsync();
    }
}
