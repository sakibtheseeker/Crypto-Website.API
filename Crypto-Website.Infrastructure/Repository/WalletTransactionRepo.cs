using AutoMapper;
using Crypto_Website.Application.DTO.WalletTransaction;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Infrastructure.Repository
{
    public class WalletTransactionRepo:IWalletTransaction
    {
        ApplicationDbContext context;
        IMapper mapper;
        public WalletTransactionRepo(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Deposite(decimal amount,int userid)
        {
            var wallet = await context.Wallets.FirstOrDefaultAsync(x=>x.UserId==userid);
            wallet.CurrentBalance += amount ;
            var dto = new WalletTransactionRequestDTO
            {
                WalletId=wallet.WalletId,
                Amount=amount,
                IsActive=true,
                TransactionType="Deposit",
                TransactionStatus="Completed" ,
                PaymentMethod="UPI",
                CreatedBy = userid,
                CreatedAt = DateTime.UtcNow
            };
            var wallettransaction = mapper.Map<WalletTransaction>(dto);
            await context.WalletTransactions.AddAsync(wallettransaction);
            await context.SaveChangesAsync();
        }

        public async Task<List<WalletTransactionResponseDTO>> GetWalletTransactions(int userid)
        {
            var wallet = await context.Wallets.FirstOrDefaultAsync(x => x.UserId == userid);
            var wlist = await context.WalletTransactions.OrderByDescending(x=>x.CreatedAt).Where(x=>x.WalletId==wallet.WalletId).ToListAsync();
            var mapwlist = mapper.Map<List<WalletTransactionResponseDTO>>(wlist);
            return mapwlist;
        }

        public async Task WithDraw(decimal amount,int userid)
        {
            var wallet = await context.Wallets.FirstOrDefaultAsync(x => x.UserId == userid);
            wallet.CurrentBalance -= amount;
            var dto = new WalletTransactionRequestDTO
            {
                WalletId = wallet.WalletId,
                Amount = amount,
                IsActive = true,
                TransactionType = "Withdrawal",
                TransactionStatus = "Completed",
                PaymentMethod = "UPI",
                CreatedBy = userid,
                CreatedAt = DateTime.UtcNow
            };
            var wallettransaction = mapper.Map<WalletTransaction>(dto);
            await context.WalletTransactions.AddAsync(wallettransaction);
            await context.SaveChangesAsync();
        }

    }
}
