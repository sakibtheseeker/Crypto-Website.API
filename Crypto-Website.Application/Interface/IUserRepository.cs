using Crypto_Website.Application.DTO;
using Crypto_Website.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Interface
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int uid);
        Task<bool> ExistsByIdAsync(int uid);

        Task<User?> GetByEmailAsync(string email);
        Task DeleteAsync(User user);

    }
}
