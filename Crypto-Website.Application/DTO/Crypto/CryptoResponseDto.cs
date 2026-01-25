using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Crypto
{
    public class CryptoResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public decimal Price { get; set; }
        public string Provider { get; set; } = null!;
    }
}
