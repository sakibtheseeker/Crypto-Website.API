using Crypto_Website.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Interface
{
    public interface IRegisterRepository
    {
        Task AddAsync(User user);
    }
}
