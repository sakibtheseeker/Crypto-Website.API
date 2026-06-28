using AutoMapper;
using Crypto_Website.Application.DTO.Portfolio;
using Crypto_Website.Application.Interface;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Infrastructure.Repository
{
    public class PortfolioRepo : IPortfolio
    {
        ApplicationDbContext context;
        IMapper mapper;
        public PortfolioRepo(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PortfolioDTO> GetPortfolio(int userId)
        {
            var Portfolio = await context.Portfolios.FirstOrDefaultAsync(x=>x.Userid==userId);
            var mappedPortfolio = mapper.Map<PortfolioDTO>(Portfolio);
            return mappedPortfolio;
        }

        
        public async Task<List<PortfolioAssetDTO>> GetPortfolioAsset(int userid)
        {
            var portfolio = await context.Portfolios.FirstOrDefaultAsync(x => x.Userid == userid);
            var portfolioAssetList =await context.PortfolioAssets.Include(x=>x.Crypto).Where(x=>x.PortfolioId==portfolio.PortfolioId ).Where(x=>x.IsActive==true).ToListAsync();
            var mappedportfolioAssetList=mapper.Map<List<PortfolioAssetDTO>>(portfolioAssetList);
            return mappedportfolioAssetList;
        }

        public async Task<PortfolioAssetsStatDTO> GetPortfolioAssetsStat(int userid)
        {
            var portfolio = await context.Portfolios.FirstOrDefaultAsync(x => x.Userid == userid);
            var portfolioAssetList = await context.PortfolioAssets.Include(x=>x.Crypto).Where(x => x.PortfolioId == portfolio!.PortfolioId).ToListAsync();
            var totalinvested =  (double)portfolioAssetList.Sum(x => x.Quantity * x.AvgBuyPrice);
            var currentportfoliovalue = (double)portfolioAssetList.Sum(x => x.Crypto.CurrentPrice * x.Quantity);
            var profitloss = currentportfoliovalue - totalinvested;
            var profitlosspercent = (profitloss / totalinvested) * 100;
            return new PortfolioAssetsStatDTO
            {
                TotalInvested = totalinvested,
                CurrentPortfolioValue = currentportfoliovalue,
                ProfitLossValue=profitloss,
                ProfitLossPrecent=profitlosspercent,


            };
        }
    }
}
