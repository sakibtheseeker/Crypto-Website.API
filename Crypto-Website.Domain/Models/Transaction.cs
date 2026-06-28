using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Domain.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Crypto")]
        public int CryptoId { get; set; }
        public string TransactionType { get; set; }
        public string transactionStatus { get; set; }
        public Decimal Price { get; set; }
        public Decimal Quantity { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public User User { get; set; }
        public Crypto Crypto { get; set; }

    }
}
