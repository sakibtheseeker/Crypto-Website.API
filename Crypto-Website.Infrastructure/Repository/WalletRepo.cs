using AutoMapper;
using Crypto_Website.Application.DTO.Wallet;
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
    public class WalletRepo : IWalletRepo
    {
        ApplicationDbContext context;
        IMapper mapper;
        public WalletRepo(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task CreateWallet(WalletRequest dto)
        {
            var data = mapper.Map<Wallet>(dto);
            await context.Wallets.AddAsync(data);
            await context.SaveChangesAsync(); 
        }

        public async Task Deposite(decimal Amount)
        {
            var wallet = await context.Wallets.FindAsync();
            wallet.CurrentBalance += Amount;
            await context.SaveChangesAsync();
        }
       

        public async Task<WalletResponse> GetBalance(int userid)
        {
            var wallet = await context.Wallets.FirstOrDefaultAsync(x=>x.UserId==userid);
            var mappedwallet = mapper.Map<WalletResponse>(wallet);
            return mappedwallet;
        }

    }
}
