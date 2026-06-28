using AutoMapper;
using Crypto_Website.Application.DTO.Favourite;
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
    public class FavouriteRepository : IFavourite
    {
        IMapper mapper;
        ApplicationDbContext _context;
        public FavouriteRepository(IMapper mapper,ApplicationDbContext context)
        {
            this.mapper = mapper;
            this._context = context;
        }

        public async Task AddFavourite(int cryptoid,int userid)
        {
            var request = new FavouriteRequestDto
            {
                CryptoId = cryptoid,
                UserId = userid,
                IsActive = true,
                CreatedBy = userid,
                CreatedAt = DateTime.Now,
            };
            var data=mapper.Map<Favorites>(request);
            var favdata = await _context.Favorites.FirstOrDefaultAsync(x => x.CryptoId == cryptoid);
            if (favdata!=null && favdata.IsActive==false)
            {
                favdata.IsActive = true;
                await _context.SaveChangesAsync();
            }
            else
            {
                await _context.Favorites.AddAsync(data);
                await _context.SaveChangesAsync();

            }
        }

        public async Task DeleteFavourite(int favId)
        {
            var fav =await _context.Favorites.FindAsync(favId);
            fav.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task<List<FavouriteResponseDto>> GetFavourite(int userId)
        {
            var favList = await _context.Favorites.Include(x=>x.Crypto).Include(x=>x.User).Where(x=>x.UserId==userId).Where(x=>x.IsActive==true).ToListAsync();
            var data=mapper.Map<List<FavouriteResponseDto>>(favList);
            return data;
        }
    }
}
