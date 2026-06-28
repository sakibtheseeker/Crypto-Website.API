using Crypto_Website.Application.DTO.Wallet;
using Crypto_Website.Application.DTO.WalletTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Interface
{
    public interface IWalletRepo
    {
        
        public Task<WalletResponse> GetBalance(int userid);
        public Task CreateWallet(WalletRequest dto);
        

    }
}
