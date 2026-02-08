using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace Crypto_Website.Domain.Models
{
    public class Crypto
    {
        [Key]
        public int Cid { get; set; }

        public string Cname { get; set; }

        public string Csymbol { get; set; }

        public string CimageUrl { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal? market_cap { get; set; }
        public decimal? total_volume { get; set; }
        public decimal? price_change_24h { get; set; }
        public decimal? price_change_percentage_24h { get; set; }

        public string ProviderName { get; set; } = "CoinGecko";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public int? DeletedBy { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

