using Crypto_Website.Domain.Models;

public interface IWalletTransactionRepository
{
    Task<List<WalletTransaction>> GetByWalletIdAsync(int wid);
    Task AddAsync(WalletTransaction tx);
}
