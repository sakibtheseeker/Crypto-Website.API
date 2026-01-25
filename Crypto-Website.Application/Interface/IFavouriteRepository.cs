using Crypto_Website.Domain.Models;

public interface IFavouriteRepository
{
    Task AddAsync(Favourite favourite);
    Task<List<Favourite>> GetByUserAsync(int uid);
    Task<Favourite> GetByIdAsync(int fid);

    Task DeleteFavAsync(Favourite fav);

}
