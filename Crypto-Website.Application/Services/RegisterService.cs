using Crypto_Website.Application.DTO.Auth;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Services
{
    public class RegisterService
    {
        private readonly IRegisterRepository _repository;

        public RegisterService(IRegisterRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(RegisterDto dto)
        {
            var user = new User
            {
                Uname = dto.Uname,
                Uemail = dto.Uemail,
                Upassword = dto.Upassword,
                CreatedBy = 1
            };

            await _repository.AddAsync(user);
        }
    }
}
