using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Dashboard
{
    public class DashboardResponseDTO
    {
        public decimal WalletBalance { get; set; }
        public decimal TotalInvested { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal ProfitLoss => CurrentValue - TotalInvested;

        public List<DashboardAssetDTO> Assets { get; set; } = [];
    }

    

}
