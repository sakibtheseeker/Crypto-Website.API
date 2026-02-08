using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Crypto
{
    public class CryptoResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Symbol { get; set; } = null!;

        public string Image { get;set; } = null!;
        public decimal Price { get; set; }
        public string Provider { get; set; } = null!;

        public decimal? Market_cap { get; set; }
        public decimal? Total_volume { get; set; }
        public decimal? Price_change_24h { get; set; }
        public decimal? Price_change_percentage_24h { get; set; }
        public decimal Quantity { get; set; }


    }
}
