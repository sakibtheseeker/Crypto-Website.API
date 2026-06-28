using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO
{
    public class GeckoApiResponseDTO
    {

        
            public string id { get; set; }
            public string symbol { get; set; }
            public string name { get; set; }
            public string image { get; set; }
            public decimal current_price { get; set; }
            public decimal? market_cap { get; set; }
            public decimal? total_volume { get; set; }
            public decimal? price_change_24h { get; set; }
            public decimal? price_change_percentage_24h { get; set; }


    }
}
