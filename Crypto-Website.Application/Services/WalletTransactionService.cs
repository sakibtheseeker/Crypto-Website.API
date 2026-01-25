using Crypto_Website.Application.DTO.Wallet;
using Crypto_Website.Application.Interface;

public class WalletTransactionService
{
    private readonly IWalletTransactionRepository _repo;

    public WalletTransactionService(IWalletTransactionRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<WalletTransactionResponseDto>> GetTransactionsAsync(int wid)
    {
        var transactions = await _repo.GetByWalletIdAsync(wid);

        return transactions.Select(t => new WalletTransactionResponseDto
        {
            Wtid = t.Wtid,
            Amount = t.Amount,
            TransactionType = t.TransactionType,
            TransactionStatus = t.TransactionStatus,
            PaymentMethod = t.PaymentMethod,
            CreatedAt = t.CreatedAt
        }).ToList();
    }
}
