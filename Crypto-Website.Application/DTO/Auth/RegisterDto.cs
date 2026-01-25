using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO.Auth
{
    public class RegisterDto
    {
        public string Uname { get; set; }
        public string Uemail { get; set; }
        public string Upassword { get; set; }
    }
}
