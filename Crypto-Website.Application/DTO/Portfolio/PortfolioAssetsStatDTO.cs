using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Portfolio
{
    public class PortfolioAssetsStatDTO
    {
        public double TotalInvested { get; set; }
        public double CurrentPortfolioValue { get; set; }
        public double ProfitLossValue { get; set; }
        public double ProfitLossPrecent { get; set; }
    }
}
