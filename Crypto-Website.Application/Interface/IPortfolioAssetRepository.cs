using Crypto_Website.Domain.Models;

namespace Crypto_Website.Application.Interface
{
    public interface IPortfolioAssetRepository
    {
        Task<PortfolioAsset?> GetAsync(int pid, int cid);
        Task AddAsync(PortfolioAsset asset);
        Task UpdateAsync(PortfolioAsset asset);
    }
}
