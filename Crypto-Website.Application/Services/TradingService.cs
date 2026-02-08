using Crypto_Website.Application.DTO.Trading;
using Crypto_Website.Application.DTO.Transaction;
using Crypto_Website.Application.Helper;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using System.Collections.Generic;

public class TradingService
{
    private readonly IWalletRepository _walletRepo;
    private readonly ICryptoRepository _cryptoRepo;
    private readonly ITransactionsHistoryRepository _historyRepo;
    private readonly IPortfolioRepository _portfolioRepo;
    private readonly IPortfolioAssetRepository _assetRepo;
    private readonly IWalletTransactionRepository _txRepo;

    public TradingService(
        IWalletRepository walletRepo,
        ICryptoRepository cryptoRepo,
        ITransactionsHistoryRepository historyRepo,
        IWalletTransactionRepository txRepo,
        IPortfolioRepository portfolioRepo,
        IPortfolioAssetRepository assetRepo)
    {
        _walletRepo = walletRepo;
        _cryptoRepo = cryptoRepo;
        _historyRepo = historyRepo;
        _portfolioRepo = portfolioRepo;
        _assetRepo = assetRepo;
        _txRepo = txRepo;
    }

    public async Task<ApiResponse<TransactionResponseDto>> BuyAsync(int uid, BuySellDto dto)
    {
        var wallet = await _walletRepo.GetByUserIdAsync(uid);
        var crypto = await _cryptoRepo.GetByIdAsync(dto.Cid);

        if (wallet == null)
            throw new Exception("Wallet not found");

        if (crypto == null)
            throw new Exception("Crypto not found");

        var portfolio = await _portfolioRepo.GetByUserIdAsync(uid);

       
        if (portfolio == null)
        {
            portfolio = new Portfolio
            {
                Uid = uid,
                IsActive = true,
                CreatedBy = uid,
                CreatedAt = DateTime.UtcNow
            };

            await _portfolioRepo.AddAsync(portfolio);

        }

        var totalCost = crypto.CurrentPrice * dto.Quantity;

        if (wallet.CurrentBal < totalCost)
            throw new Exception("Insufficient balance");

        wallet.CurrentBal -= totalCost;
        wallet.UpdatedBy = uid;
        wallet.UpdatedAt = DateTime.UtcNow;

        await _walletRepo.UpdateAsync(wallet);

        await _txRepo.AddAsync(new WalletTransaction
        {
            Wid = wallet.Wid,
            Amount = totalCost,
            TransactionType = "DEBIT",
            TransactionStatus = "SUCCESS",
            PaymentMethod = "UPI",
            Description = $"{crypto.Cname} Purchased",
            CreatedBy = uid
        });

        var asset = await _assetRepo.GetAsync(portfolio.Pid, dto.Cid);

        if (asset == null)
        {
            asset = new PortfolioAsset
            {
                Pid = portfolio.Pid,
                Cid = dto.Cid,
                Quantity = dto.Quantity,
                AvgBuyPrice = crypto.CurrentPrice,
                Image=crypto.CimageUrl,
                Symbol=crypto.Csymbol,
                Market_cap=crypto.market_cap,
                Total_volume=crypto.total_volume,
                Price_change_24h=crypto.price_change_24h,
                Price_change_percentage_24h=crypto.price_change_percentage_24h,
                CreatedBy = uid,
                CreatedAt = DateTime.UtcNow
            };

            await _assetRepo.AddAsync(asset);
        }
        else
        {
            var totalQty = asset.Quantity + dto.Quantity;

            asset.AvgBuyPrice =
                ((asset.AvgBuyPrice * asset.Quantity) +
                (crypto.CurrentPrice * dto.Quantity))
                / totalQty;

            asset.Quantity = totalQty;
            asset.UpdatedBy = uid;
            asset.UpdatedAt = DateTime.UtcNow;

            await _assetRepo.UpdateAsync(asset);
        }

        var result = await _historyRepo.AddAsync(new TransactionsHistory
        {
            Uid = uid,
            Cid = dto.Cid,
            TransactionType = "DEBIT",
            TransactionStatus = "SUCCESS",
            Price = crypto.CurrentPrice,
            Quantity = dto.Quantity,
            Description= $"{crypto.Cname} Purchased",
            CreatedAt = DateTime.UtcNow
        });

        

        return ApiResponse<TransactionResponseDto>.SuccessResponse(
            new TransactionResponseDto
            {
                Tid = result.Tid,
                Cid = result.Cid,
                Price = result.Price,
                Quantity = result.Quantity,
                CreatedAt = result.CreatedAt
            },
            "Buy order Successful"
        );
    }


    public async Task<ApiResponse<TransactionResponseDto>> SellAsync(int uid, BuySellDto dto)
    {
        var wallet = await _walletRepo.GetByUserIdAsync(uid);
        var crypto = await _cryptoRepo.GetByIdAsync(dto.Cid);

        var portfolio = await _portfolioRepo.GetByUserIdAsync(uid);
        if (portfolio == null)
            throw new Exception("Portfolio not found");

        var asset = await _assetRepo.GetAsync(portfolio.Pid, dto.Cid);

        if (asset == null || asset.Quantity < dto.Quantity)
            throw new Exception("Not enough quantity");

        wallet.CurrentBal += crypto.CurrentPrice * dto.Quantity;
        var totalAmount = crypto.CurrentPrice * dto.Quantity;
        await _walletRepo.UpdateAsync(wallet);

        await _txRepo.AddAsync(new WalletTransaction
        {
            Wid = wallet.Wid,
            Amount = totalAmount,
            TransactionType = "CREDIT",
            TransactionStatus = "SUCCESS",
            PaymentMethod = "UPI",
            Description = $"{crypto.Cname} Sold",
            CreatedBy = uid
        });

        var result = await _historyRepo.AddAsync(new TransactionsHistory
        {
            Uid = uid,
            Cid = dto.Cid,
            TransactionType = "CREDIT",
            TransactionStatus = "SUCCESS",
            Price = crypto.CurrentPrice,
            Quantity = dto.Quantity,
            Description = $"{crypto.Cname} Purchased",
            CreatedAt = DateTime.UtcNow
        });

        asset.Quantity -= dto.Quantity;
        asset.UpdatedBy = uid;
        asset.UpdatedAt = DateTime.UtcNow;


        await _assetRepo.UpdateAsync(asset);

        var responseDto = new TransactionResponseDto
        {
            Tid = result.Tid,
            Cid = result.Cid,
            Price = result.Price,
            Quantity = result.Quantity,
            CreatedAt = DateTime.UtcNow
        };

        return ApiResponse<TransactionResponseDto>
            .SuccessResponse(responseDto, "Sell order Successful");
    }

    public async Task<ApiResponse<List<TransactionResponseDto>>> GetTransactionsAsync(int uid)
    {
        var transactions = await _historyRepo.GetByUserIdAsync(uid);

        var result = transactions.Select(t => new TransactionResponseDto
        {
            Tid = t.Tid,
            Cid = t.Cid,
            TransactionType = t.TransactionType,
            TransactionStatus = t.TransactionStatus,
            Price = t.Price,
            Quantity = t.Quantity,
            CreatedAt = t.CreatedAt
        }).ToList();

        return ApiResponse<List<TransactionResponseDto>>
            .SuccessResponse(result, "Transaction Fetched Successfully");
    }
}
