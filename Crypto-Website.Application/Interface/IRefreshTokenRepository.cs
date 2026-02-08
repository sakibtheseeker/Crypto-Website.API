using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto_Website.Domain.Models;

namespace Crypto_Website.Application.Interface
{
    public interface IRefreshTokenRepository
    {
        Task SaveAsync(int uid, string token, DateTime expiresAt);
        Task<RefreshToken?> GetAsync(string token);
        Task RevokeAsync(RefreshToken token);
    }

}
