using AutoMapper;
using Crypto_Website.Application.DTO.Portfolio;
using Crypto_Website.Application.DTO.Transaction;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Infrastructure.Repository
{
    public class TransactionRepo : ITransactionRepo
    {
        ApplicationDbContext context;
        IMapper mapper;
        public TransactionRepo(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Buy(TransactionDTO dto, int userid)
        {
            var portfolio = await context.Portfolios.FirstOrDefaultAsync(x => x.Userid == userid);

            
            var transactionbuydto = new TransactionBuyDTO()
            {
                UserId =userid,
                CryptoId=dto.CryptoId,
                transactionStatus="Completed",
                TransactionType="buy",
                Price=dto.Price,
                Quantity=dto.Quantity,
                CreatedBy=userid,
                CreatedAt=DateTime.UtcNow
            };
            var buydata = mapper.Map<Transaction>(transactionbuydto);
            await context.Transactions.AddAsync(buydata);
            await context.SaveChangesAsync();
            var portfolioassetcheck = await context.PortfolioAssets.Where(x => x.PortfolioId == portfolio.PortfolioId).Where(x => x.CryptoId == dto.CryptoId).Where(x => x.IsActive == true).FirstOrDefaultAsync();
            if (portfolioassetcheck != null)
            {
                portfolioassetcheck.Quantity += dto.Quantity;
                portfolioassetcheck.AvgBuyPrice = dto.Price;
            }
            else
            {

                    var PortfolioAsset = new PortfolioAsset
                    {
                        PortfolioId = portfolio.PortfolioId,
                        CryptoId = dto.CryptoId,
                        Quantity = dto.Quantity,
                        AvgBuyPrice = dto.Price,
                        IsActive = true,
                        CreatedAt = transactionbuydto.CreatedAt,
                        CreatedBy = transactionbuydto.CreatedBy,
                    };
                await context.AddAsync(PortfolioAsset);
                await context.SaveChangesAsync();
            }
            var wallet = await context.Wallets.FirstOrDefaultAsync(x => x.UserId == userid);
            wallet.CurrentBalance -= dto.Price;
            await context.SaveChangesAsync();
            
           
        }

        public async Task<List<TransactionResponseDTO>> GetAll(int UserId)
        {
            var tList = await context.Transactions.Include(x=>x.User).Include(x=>x.Crypto).OrderByDescending(e=>e.CreatedAt).Take(5).Where(e => e.UserId == UserId).ToListAsync();
            
            var list =  mapper.Map<List<TransactionResponseDTO>>(tList);
            foreach (var item in list)
            {
                var portfolioAsset = await context.PortfolioAssets.FirstOrDefaultAsync(x => x.CryptoId == item.CryptoId);
                item.AvgBuyPrice = portfolioAsset.AvgBuyPrice;

            }
            return list;
        }

        public async Task<TransactionResponseDTO> GetById(int TransactionId)
        {
            var transaction = await context.Transactions.FindAsync(TransactionId);
            var mappedtransaction = mapper.Map<TransactionResponseDTO>(transaction);
            return mappedtransaction;
        }

        public async Task Sell(TransactionDTO dto, int userid)
        {
            var transactionselldto = new TransactionSellDTO
            {
                UserId = userid,
                CryptoId = dto.CryptoId,
                Price = dto.Price,
                Quantity = dto.Quantity,
                transactionStatus = "Completed",
                TransactionType = "buy",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userid,
            };
            var sellData = mapper.Map<Transaction>(transactionselldto);
            await context.Transactions.AddAsync(sellData);
            await context.SaveChangesAsync();
            var portfolio = await context.Portfolios.FirstOrDefaultAsync(x=> x.Userid == userid);
            var portfolioasset = await context.PortfolioAssets.Where(x => x.PortfolioId == portfolio.PortfolioId).Where(x => x.CryptoId == dto.CryptoId).FirstOrDefaultAsync();
            if (portfolioasset.Quantity == dto.Quantity)
            {
                portfolioasset.Quantity -= dto.Quantity;
                portfolioasset.IsActive = false;
            }
            else
            {
                portfolioasset.Quantity -= dto.Quantity;
            }
                await context.SaveChangesAsync();
            var wallet = await context.Wallets.FirstOrDefaultAsync(x => x.UserId == userid);
            wallet.CurrentBalance += dto.Price;
            await context.SaveChangesAsync();
            

        }
    }
}
