using AutoMapper;
using Crypto_Website.Application.DTO;
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
    public class CryptoRepo : ICrypto
    {
        private readonly ApplicationDbContext _context;
        IMapper _mapper;
        public CryptoRepo(ApplicationDbContext context,IMapper mapper) 
        {
            _context=context;
            _mapper=mapper;
        }
        public void Add(CryptoRequestDto dto)
        {
            var data = _mapper.Map<Crypto>(dto);
            _context.Cryptos.Add(data);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CryptoResponseDto> Get(int id)
        {
            var data = await _context.Cryptos.FindAsync(id);
            var coin=_mapper.Map<CryptoResponseDto>(data);
            return coin;
        }

        public async Task<List<CryptoResponseDto>> GetAll(int pageno,int pagesize,int userid)
        {
            var data = await _context.Cryptos.Skip((pageno-1)*pagesize).Take(pagesize).ToListAsync();
            var coins = _mapper.Map<List<CryptoResponseDto>>(data);
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(x => x.Userid == userid);
            var assets = await _context.PortfolioAssets
            .Where(x => x.PortfolioId == portfolio.PortfolioId)
            .ToListAsync();
            foreach (var item in coins)
            {
                var asset = assets.FirstOrDefault(x=>x.CryptoId==item.CryptoId);
                item.Quantity = asset?.Quantity ?? 0;
            }
            return coins;
        }

        public void Update(CryptoRequestDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
