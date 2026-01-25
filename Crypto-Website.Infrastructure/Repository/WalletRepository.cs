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

    public async Task<Wallet> GetByUserIdAsync(int uid)
    {
        return await _db.Wallets.FirstAsync(w => w.Uid == uid);
    }

    public async Task UpdateAsync(Wallet wallet)
    {
        _db.Wallets.Update(wallet);
        await _db.SaveChangesAsync();
    }
}
