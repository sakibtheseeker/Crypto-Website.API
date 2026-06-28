using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Domain.Models
{
    public class User
    {
        [Key]
        public int Userid { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<Favorites> Favorites { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Wallet Wallet { get; set; }
        public Portfolio Portfolio { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }

    }
}
