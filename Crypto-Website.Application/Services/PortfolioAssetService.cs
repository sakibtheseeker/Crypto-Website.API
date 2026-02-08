using Crypto_Website.Application.DTO.Portfolio;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;

namespace Crypto_Website.Application.Services
{
    public class PortfolioAssetService
    {
        private readonly IPortfolioAssetRepository _repo;

        public PortfolioAssetService(IPortfolioAssetRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<PortfolioAssetResponseDto>> GetAssetsAsync(int pid)
        {
            return await _repo.GetByPortfolioIdAsync(pid);
        }

        public async Task AddOrUpdateAsync(
            int pid,
            int cid,
            decimal quantity,
            int uid)
        {
            var asset = await _repo.GetAsync(pid, cid);

            if (asset == null)
            {
                await _repo.AddAsync(new PortfolioAsset
                {
                    Pid = pid,
                    Cid = cid,
                    Quantity = quantity,
                    CreatedBy = uid
                });
            }
            else
            {
                asset.Quantity += quantity;
                asset.UpdatedBy = uid;
                asset.UpdatedAt = DateTime.UtcNow;

                await _repo.UpdateAsync(asset);
            }
        }
    }
}
