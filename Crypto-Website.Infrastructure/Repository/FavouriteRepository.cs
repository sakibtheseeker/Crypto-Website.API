using Crypto_Website.Application.DTO.Favourite;
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

    public async Task AddAsync(Favourite favourite)
    {
        //if(favourite.Cid==favourite.Crypto.Cid)
        //{
        //    return Ok(new { message = "Favourite Already Exist " });
        //}
        _context.Favourites.Add(favourite);
        await _context.SaveChangesAsync();
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


}
