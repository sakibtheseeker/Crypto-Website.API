using Crypto_Website.Application.DTO.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Interface
{
    public interface IDashboardRepository
    {
        Task<decimal> GetWalletBalanceAsync(int uid);
        Task<List<DashboardAssetDTO>> GetPortfolioAssetsAsync(int uid);
    }
}

