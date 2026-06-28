using Crypto_Website.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Interface
{
    public interface IAuth
    {
        public Task RegisterUser(RegisterDTO dto);
        public Task<LoginResponseDTO> LoginUser(LoginDTO dto);
        public Task<LoginResponseDTO> ValidateRefreshToken(string token);
    }
}
