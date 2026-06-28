using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO
{
    public class RefreshTokenDTO
    {
        public string Token { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime ExpireTime { get; set; }
        public int UserId { get; set; }
    }
}
