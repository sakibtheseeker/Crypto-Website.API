using Crypto_Website.Application.DTO.WalletTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Interface
{
    public interface IWalletTransaction
    {
        public Task WithDraw(decimal amount,int userid);
        public Task Deposite(decimal amount, int userid);
        public Task<List<WalletTransactionResponseDTO>> GetWalletTransactions(int userid);
    }
}
