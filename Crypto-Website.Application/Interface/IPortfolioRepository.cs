using Crypto_Website.Domain.Models;

namespace Crypto_Website.Application.Interface
{
    public interface IPortfolioRepository
    {
        Task<Portfolio?> GetByUserIdAsync(int uid);
        Task AddAsync(Portfolio portfolio);
    }
}
