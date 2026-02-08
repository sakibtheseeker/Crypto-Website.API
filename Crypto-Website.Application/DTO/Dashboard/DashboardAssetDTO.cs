using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Dashboard
{
    public class DashboardAssetDTO
    {
        public int Cid { get; set; }
        public string CryptoName { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal InvestedValue => Quantity * BuyPrice;
        public string Image { get; set; } = string.Empty;
        public decimal Value => Quantity * CurrentPrice;
    }
}
