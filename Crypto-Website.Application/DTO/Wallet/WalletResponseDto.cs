using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Wallet
{
    public class WalletResponseDto
    {
        public int Wid { get; set; }
        public decimal CurrentBal { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

