using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Transaction
{
    public class TransactionResponseDto
    {
        public int Tid { get; set; }
        public int Cid { get; set; }
        public string TransactionType { get; set; }
        public string TransactionStatus { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
