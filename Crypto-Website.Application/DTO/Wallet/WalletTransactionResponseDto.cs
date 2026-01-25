using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Wallet
{
    public class WalletTransactionResponseDto
    {
        public int Wtid { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string TransactionStatus { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

