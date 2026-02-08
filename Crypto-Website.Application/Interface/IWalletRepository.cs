using Crypto_Website.Domain.Models;
using System.Threading.Tasks;

public interface IWalletRepository
{
    Task<Wallet> GetByUserIdAsync(int uid);
    Task UpdateAsync(Wallet wallet);

    Task AddAsync(Wallet wallet);
}
