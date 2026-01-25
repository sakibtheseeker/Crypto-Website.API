using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(int uid)
        {
            return await _repository.GetByIdAsync(uid);
        }

        public async Task<bool> DeleteUserAsync(int uid)
        {
            var user = await _repository.GetByIdAsync(uid);

            if (user == null)
                return false;

            await _repository.DeleteAsync(user);
            return true;
        }
    }
}
