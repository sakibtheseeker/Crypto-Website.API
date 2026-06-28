using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Transaction
{
    public class TransactionResponseDTO
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CryptoId { get; set; }
        public string CryptoName { get; set; }
        public string CryptoSymbol { get; set; }
        public string TransactionType { get; set; }
        public string transactionStatus { get; set; }
        public Decimal Price { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal AvgBuyPrice { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
