using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Transaction
{
    public class TransactionDTO
    {
        public int CryptoId { get; set; }
        
        public Decimal Price { get; set; }
        public Decimal Quantity { get; set; }
    }
}
