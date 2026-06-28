using Crypto_Website.Application.DTO.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Interface
{
    public interface ITransactionRepo
    {
        public Task Buy(TransactionDTO dto,int userid);
        public Task Sell(TransactionDTO dto,int userid);
        public Task<List<TransactionResponseDTO>> GetAll(int UserId);
        public Task<TransactionResponseDTO> GetById(int TransactionId);

    }
}
