using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Favourite
{
    public class FavouriteDto
    {
        public int Fid { get; set; }
        public int Cid { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string image { get; set; }
        public decimal CurrentPrice { get; set; }

        public decimal? market_cap { get; set; }
        public decimal? total_volume { get; set; }
        public decimal? price_change_24h { get; set; }
        public decimal? price_change_percentage_24h { get; set; }
    }

}
