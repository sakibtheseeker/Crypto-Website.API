using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Domain.Models
{
    public class WalletTransaction
    {
        [Key]
        public int Wtid { get; set; }
        public int Wid { get; set; }

        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = null!;
        public string? TransactionStatus { get; set; }      
        public string PaymentMethod { get; set; } = null!;

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? CreatedBy { get; set; }

        public bool IsActive { get; set; } = true;

        public Wallet Wallet { get; set; } = null!;
    }
}
