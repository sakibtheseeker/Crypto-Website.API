using Crypto_Website.Application.DTO.Portfolio;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;

public class PortfolioService
{
    private readonly IPortfolioRepository _portfolioRepo;
    private readonly IPortfolioAssetRepository _assetRepo;

    public PortfolioService(
        IPortfolioRepository portfolioRepo,
        IPortfolioAssetRepository assetRepo)
    {
        _portfolioRepo = portfolioRepo;
        _assetRepo = assetRepo;
    }

    public async Task<PortfolioResponseDto> GetPortfolioAsync(int uid)
    {
        var portfolio = await _portfolioRepo.GetByUserIdAsync(uid);

        if (portfolio == null)
        {
            portfolio = new Portfolio
            {
                Uid = uid,
                CreatedBy = uid
            };

            await _portfolioRepo.AddAsync(portfolio);


        }

        var assets = await _assetRepo.GetByPortfolioIdAsync(portfolio.Pid);

        return new PortfolioResponseDto
        {
            Pid = portfolio.Pid,
            Assets = assets
        };
    }
}
