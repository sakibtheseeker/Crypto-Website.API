using Crypto_Website.Domain.Models;
using Crypto_Website.Application.DTO.Portfolio;

namespace Crypto_Website.Application.Interface
{
    public interface IPortfolioRepository
    {
        Task<Portfolio?> GetAsync(int uid, int cid);
        Task<List<PortfolioResponseDto>> GetByUserIdAsync(int uid);

        Task AddAsync(Portfolio portfolio);
        Task UpdateAsync(Portfolio portfolio);
    }
}
