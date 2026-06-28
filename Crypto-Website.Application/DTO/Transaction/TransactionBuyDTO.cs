using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Transaction
{
    public class TransactionBuyDTO
    {
        public int UserId { get; set; }
        public int CryptoId { get; set; }
        public string TransactionType { get; set; }
        public string transactionStatus { get; set; }
        public Decimal Price { get; set; }
        public Decimal Quantity { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
