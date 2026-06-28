using Crypto_Website.Application.DTO.Portfolio;

namespace Crypto_Website.Application.Interface
{
    public interface IPortfolio
    {
        public Task<PortfolioDTO> GetPortfolio(int userId);
        public Task<List<PortfolioAssetDTO>> GetPortfolioAsset(int portfolioId);
        public Task<PortfolioAssetsStatDTO> GetPortfolioAssetsStat(int userid);
        
    }
}
