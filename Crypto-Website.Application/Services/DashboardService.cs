using Crypto_Website.Application.DTO.Dashboard;
using Crypto_Website.Application.Helper;
using Crypto_Website.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Services
{
    public class DashboardService 
    {
        private readonly IDashboardRepository _dashboardRepo;

        public DashboardService(IDashboardRepository dashboardRepo)
        {
            _dashboardRepo = dashboardRepo;
        }

        public async Task<ApiResponse<DashboardResponseDTO>> GetDashboardAsync(int uid)
        {
            var walletBalance = await _dashboardRepo.GetWalletBalanceAsync(uid);
            var assets = await _dashboardRepo.GetPortfolioAssetsAsync(uid);

            var totalInvested = assets.Sum(a => a.InvestedValue);
            var currentValue = assets.Sum(a => a.Value);

            return ApiResponse<DashboardResponseDTO>.SuccessResponse(

                new DashboardResponseDTO
                {
                    WalletBalance = walletBalance,
                    TotalInvested = totalInvested,
                    CurrentValue = currentValue,
                    Assets = assets
                },
                "Dashboard Fetched Successfully"
                );


                
        }

    }
}
