using Crypto_Website.Application.DTO.Trading;
using Crypto_Website.Application.DTO.Transaction;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;

public class TradingService
{
    private readonly IWalletRepository _walletRepo;
    private readonly ICryptoRepository _cryptoRepo;
    private readonly ITransactionsHistoryRepository _historyRepo;
    private readonly IPortfolioRepository _portfolioRepo;

    public TradingService(
        IWalletRepository walletRepo,
        ICryptoRepository cryptoRepo,
        ITransactionsHistoryRepository historyRepo,
        IPortfolioRepository portfolioRepo)
    {
        _walletRepo = walletRepo;
        _cryptoRepo = cryptoRepo;
        _historyRepo = historyRepo;
        _portfolioRepo = portfolioRepo;
    }

    public async Task BuyAsync(int uid, BuySellDto dto)
    {
        var wallet = await _walletRepo.GetByUserIdAsync(uid);
        var crypto = await _cryptoRepo.GetByIdAsync(dto.Cid);
        var portfolio = await _portfolioRepo.GetAsync(uid, dto.Cid);

        var totalCost = crypto.CurrentPrice * dto.Quantity;
        if (wallet.CurrentBal < totalCost)
            throw new Exception("Insufficient balance");

        wallet.CurrentBal -= totalCost;
        await _walletRepo.UpdateAsync(wallet);

        if (portfolio == null)
        {
            portfolio = new Portfolio
            {
                Uid = uid,
                Cid = dto.Cid,
                Quantity = dto.Quantity,
                AvgBuyPrice = crypto.CurrentPrice
            };
            await _portfolioRepo.AddAsync(portfolio);
        }
        else
        {
            portfolio.Buy(dto.Quantity, crypto.CurrentPrice);
            await _portfolioRepo.UpdateAsync(portfolio);
        }

        await _historyRepo.AddAsync(new TransactionsHistory
        {
            Uid = uid,
            Cid = dto.Cid,
            TransactionType = "BUY",
            TransactionStatus = "SUCCESS",
            Price = crypto.CurrentPrice,
            Quantity = dto.Quantity
        });
    }

    public async Task SellAsync(int uid, BuySellDto dto)
    {
        var wallet = await _walletRepo.GetByUserIdAsync(uid);
        var crypto = await _cryptoRepo.GetByIdAsync(dto.Cid);
        var portfolio = await _portfolioRepo.GetAsync(uid, dto.Cid);

        if (portfolio == null || portfolio.Quantity < dto.Quantity)
            throw new Exception("Not enough quantity");

        wallet.CurrentBal += crypto.CurrentPrice * dto.Quantity;
        await _walletRepo.UpdateAsync(wallet);

        portfolio.Sell(dto.Quantity);
        await _portfolioRepo.UpdateAsync(portfolio);

        await _historyRepo.AddAsync(new TransactionsHistory
        {
            Uid = uid,
            Cid = dto.Cid,
            TransactionType = "SELL",
            TransactionStatus = "SUCCESS",
            Price = crypto.CurrentPrice,
            Quantity = dto.Quantity
        });
    }
    public async Task<List<TransactionResponseDto>> GetTransactionsAsync(int uid)
    {
        var transactions = await _historyRepo.GetByUserIdAsync(uid);

        return transactions.Select(t => new TransactionResponseDto
        {
            Tid = t.Tid,
            Cid = t.Cid,
            TransactionType = t.TransactionType,
            TransactionStatus = t.TransactionStatus,
            Price = t.Price,
            Quantity = t.Quantity,
            CreatedAt = t.CreatedAt
        }).ToList();
    }

}
