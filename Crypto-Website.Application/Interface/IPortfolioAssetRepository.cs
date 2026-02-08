using Crypto_Website.Domain.Models;
using Crypto_Website.Application.DTO.Portfolio;

namespace Crypto_Website.Application.Interface
{
    public interface IPortfolioAssetRepository
    {
        Task<List<PortfolioAssetResponseDto>> GetByPortfolioIdAsync(int pid);
        Task<PortfolioAsset?> GetAsync(int pid, int cid);
        Task AddAsync(PortfolioAsset asset);
        Task UpdateAsync(PortfolioAsset asset);
    }
}
