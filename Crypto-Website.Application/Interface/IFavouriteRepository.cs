using Crypto_Website.Domain.Models;
using Crypto_Website.Application.Helper;
using Crypto_Website.Application.DTO.Favourite;

public interface IFavouriteRepository
{
    Task<Favourite> AddAsync(Favourite favourite);
    Task UpdateAsync(Favourite favourite);
    Task<List<Favourite>> GetByUserAsync(int uid);
    Task<Favourite?> GetByIdAsync(int fid);
    Task<Favourite?> GetByUserAndCryptoAsync(int uid, int cid);
    Task DeleteFavAsync(Favourite fav);
}

