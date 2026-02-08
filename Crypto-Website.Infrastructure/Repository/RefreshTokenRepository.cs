using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDBContext _context;

        public RefreshTokenRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(int uid, string token, DateTime expiresAt)
        {
            var refreshToken = new RefreshToken
            {
                Uid = uid,
                Token = token,
                ExpiresAt = expiresAt,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r =>
                    r.Token == token &&
                    r.IsActive &&
                    r.RevokedAt == null);
        }

        public async Task RevokeAsync(RefreshToken token)
        {
            token.IsActive = false;
            token.RevokedAt = DateTime.UtcNow;

            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }
    }
}
