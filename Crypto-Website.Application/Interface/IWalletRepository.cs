using Crypto_Website.Domain.Models;

public interface IWalletRepository
{
    Task<Wallet> GetByUserIdAsync(int uid);
    Task UpdateAsync(Wallet wallet);
}
