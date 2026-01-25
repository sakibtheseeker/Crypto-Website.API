using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Crypto_Website.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .Where(u => u.IsActive)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int uid)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Uid == uid && u.IsActive);
        }

        public async Task<bool> ExistsByIdAsync(int uid)
        {
            return await _context.Users
                .AnyAsync(u => u.Uid == uid && u.IsActive);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Uemail == email && u.IsActive);
        }

        //public async Task<User> GetUserNameAsync(string uname)
        //{
        //    return await _context.Users
        //        .FirstOrDefaultAsync(u => u.Uname == uname && u.IsActive);

        //}

        public async Task DeleteAsync(User user)
        {
            user.IsActive = false;
            user.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

    }
}
