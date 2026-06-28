using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Favourite
{
    public class FavouriteResponseDto
    {
        public int FavouriteId { get; set; }
        public int UserId { get; set; }
        public int CryptoId { get; set; }
        public string CryptoName { get; set; }
        public string CryptoSymbol { get; set; }
        public string CryptoImage { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal? MarketCap { get; set; }
        public decimal? TotalVolume { get; set; }
        public decimal? PriceChange24h { get; set; }
        public decimal? PriceChangepercentage24h { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
