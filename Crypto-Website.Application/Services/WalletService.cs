using Crypto_Website.Application.DTO.Wallet;
using Crypto_Website.Application.Helper;
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

    public async Task<ApiResponse<WalletResponseDto>> GetWalletAsync(int uid)
    {
        var wallet = await _walletRepo.GetByUserIdAsync(uid);

        if (wallet == null)
        {
            return ApiResponse<WalletResponseDto>
                .SuccessResponse(null, "Wallet not found");
        }


        var result = new WalletResponseDto
        {
            Wid = wallet.Wid,
            CurrentBal = wallet.CurrentBal,
            CreatedAt = wallet.CreatedAt
        };

        return ApiResponse<WalletResponseDto>
            .SuccessResponse(result, "Wallet fetched successfully");
    }

    public async Task<ApiResponse<List<WalletTransactionResponseDto>>> GetTransactionsAsync(int uid)
    {
        var wallet = await _walletRepo.GetByUserIdAsync(uid);

        if (wallet == null)
        {
            return ApiResponse<List<WalletTransactionResponseDto>>
                .SuccessResponse(new List<WalletTransactionResponseDto>(), "No wallet found");
        }


        var transactions = await _txRepo.GetByWalletIdAsync(wallet.Wid);

        var result = transactions.Select(t => new WalletTransactionResponseDto
        {
            Wtid = t.Wtid,
            Amount = t.Amount,
            TransactionType = t.TransactionType,
            TransactionStatus = t.TransactionStatus,
            PaymentMethod = t.PaymentMethod,
            Description = t.Description,
            CreatedAt = t.CreatedAt
        }).ToList();

        return ApiResponse<List<WalletTransactionResponseDto>>
            .SuccessResponse(result, "Transactions fetched successfully");
    }

    public async Task<ApiResponse<object>> DepositAsync(int uid, decimal amount)
    {
        if (amount <= 0)
            return null;

        var wallet = await _walletRepo.GetByUserIdAsync(uid);

        if (wallet == null)
            return null;

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
            Description = "Money Deposited",
            CreatedBy = uid
        });

        return ApiResponse<object>
            .SuccessResponse(null, "Deposit successful");
    }

    public async Task<ApiResponse<object>> WithdrawAsync(int uid, decimal amount)
    {
        if (amount <= 0)
            return null;

        var wallet = await _walletRepo.GetByUserIdAsync(uid);

        if (wallet == null)
            return null;

        if (wallet.CurrentBal < amount)
            return null;

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
            Description ="Money Withdrawn",
            CreatedBy = uid
        });

        return ApiResponse<object>
            .SuccessResponse(null, "Withdraw successful");
    }
}
