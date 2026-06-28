using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO
{
    public class LoginResponseDTO
    {
        public string UserEmail { get; set; }
        public string token { get; set; }
        public string ExpireTime { get; set; }
        public string RefreshToken { get; set; }
    }
}
