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
        public string CryptoName { get; set; }
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
    }

}
