using Crypto_Website.Application.DTO.Favourite;
using Crypto_Website.Application.Helper;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class FavouriteRepository : IFavouriteRepository
{
    private readonly ApplicationDBContext _context;

    public FavouriteRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Favourite> AddAsync(Favourite favourite)
    {
        _context.Favourites.Add(favourite);
        await _context.SaveChangesAsync();

        return await _context.Favourites
            .Include(f => f.Crypto)
            .FirstAsync(f => f.Fid == favourite.Fid);
    }

    public async Task<Favourite?> GetByUserAndCryptoAsync(int uid, int cid)
    {
        return await _context.Favourites
            .Include(f => f.Crypto)
            .FirstOrDefaultAsync(f => f.Uid == uid && f.Cid == cid);
    }


    public async Task<List<Favourite>> GetByUserAsync(int uid)
    {
        return await _context.Favourites
            .Include(f => f.Crypto)   
            .Where(f => f.Uid == uid && f.IsActive)
            .ToListAsync();
    }


    public async Task<Favourite?> GetByIdAsync(int fid)
    {
        return await _context.Favourites
            .Include(f => f.Crypto)
            .FirstOrDefaultAsync(f => f.Fid == fid && f.IsActive);
            
    }

    public async Task DeleteFavAsync(Favourite favourite)
    {
        favourite.IsActive=false;
        favourite.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Favourite favourite)
    {
        _context.Favourites.Update(favourite);
        await _context.SaveChangesAsync();
    }

}
