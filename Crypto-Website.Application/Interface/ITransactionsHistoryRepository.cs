using Crypto_Website.Domain.Models;

public interface ITransactionsHistoryRepository
{
    Task <TransactionsHistory> AddAsync(TransactionsHistory history);

    Task<List<TransactionsHistory>> GetByUserIdAsync(int uid);
}
