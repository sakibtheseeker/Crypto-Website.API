using Crypto_Website.Application.DTO.Wallet;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;

public class WalletService
{
    private readonly IWalletRepository _walletRepo;
    private readonly IWalletTransactionRepository _txRepo;

    public WalletService(
        IWalletRepository walletRepo,
        IWalletTransactionRepository txRepo)
    {
        _walletRepo = walletRepo;
        _txRepo = txRepo;
    }


    public async Task<WalletResponseDto> GetWalletAsync(int uid)
    {
        var wallet = await _walletRepo.GetByUserIdAsync(uid);

        if (wallet == null)
            throw new Exception("Wallet not found");

        return new WalletResponseDto
        {
            Wid = wallet.Wid,
            CurrentBal = wallet.CurrentBal,
            CreatedAt = wallet.CreatedAt,
        };
    }

    public async Task<List<WalletTransactionResponseDto>> GetTransactionsAsync(int uid)
    {
        var wallet = await _walletRepo.GetByUserIdAsync(uid);

        if (wallet == null)
            throw new Exception("Wallet not found");

        var transactions = await _txRepo.GetByWalletIdAsync(wallet.Wid);

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

    public async Task DepositAsync(int uid, decimal amount)
    {
        if (amount <= 0)
            throw new Exception("Invalid amount");

        var wallet = await _walletRepo.GetByUserIdAsync(uid);

        if (wallet == null)
            throw new Exception("Wallet not found");

        wallet.CurrentBal += amount;
        wallet.UpdatedAt = DateTime.UtcNow;
        wallet.UpdatedBy = uid;

        await _walletRepo.UpdateAsync(wallet);

        await _txRepo.AddAsync(new WalletTransaction
        {
            Wid = wallet.Wid,
            Amount = amount,
            TransactionType = "CREDIT",
            TransactionStatus = "SUCCESS",
            PaymentMethod = "UPI",
            CreatedBy = uid
        });
    }

    public async Task WithdrawAsync(int uid, decimal amount)
    {
        if (amount <= 0)
            throw new Exception("Invalid amount");

        var wallet = await _walletRepo.GetByUserIdAsync(uid);

        if (wallet == null)
            throw new Exception("Wallet not found");

        if (wallet.CurrentBal < amount)
            throw new Exception("Insufficient balance");

        wallet.CurrentBal -= amount;
        wallet.UpdatedAt = DateTime.UtcNow;
        wallet.UpdatedBy = uid;

        await _walletRepo.UpdateAsync(wallet);

        await _txRepo.AddAsync(new WalletTransaction
        {
            Wid = wallet.Wid,
            Amount = amount,
            TransactionType = "DEBIT",
            TransactionStatus = "SUCCESS",
            PaymentMethod = "WALLET",
            CreatedBy = uid
        });
    }
}
