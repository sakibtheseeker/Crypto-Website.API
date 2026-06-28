using Crypto_Website.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Interface
{
    public interface IGeckoCoinservice
    {
        Task<List<GeckoApiResponseDTO>> GetCoins();
    }
}
