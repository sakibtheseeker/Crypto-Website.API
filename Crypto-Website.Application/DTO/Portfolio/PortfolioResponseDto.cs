using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Portfolio
{
    public class PortfolioResponseDto
    {
        public int Pid { get; set; }
        public string CryptoName { get; set; }
        public decimal Quantity { get; set; }
        public decimal AvgBuyPrice { get; set; }
    }

}
