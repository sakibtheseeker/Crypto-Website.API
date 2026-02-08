using Crypto_Website.Application.DTO.Favourite;
using Crypto_Website.Application.Helper;
using Crypto_Website.Domain.Models;

public class FavouriteService
{
    private readonly IFavouriteRepository _repo;

    public FavouriteService(IFavouriteRepository repo)
    {
        _repo = repo;
    }

    public async Task<ApiResponse<FavouriteDto>> AddAsync(int uid, int cid)
    {
        var existing = await _repo.GetByUserAndCryptoAsync(uid, cid);

        Favourite fav;

        if (existing != null)
        {
            // Reactivate soft deleted favourite
            existing.IsActive = true;
            existing.DeletedAt = null;
            existing.UpdatedBy = uid;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(existing);
            fav = existing;
        }
        else
        {
            fav = await _repo.AddAsync(new Favourite
            {
                Uid = uid,
                Cid = cid,
                IsActive = true,
                CreatedBy = uid,
                CreatedAt = DateTime.UtcNow
            });
        }

        return ApiResponse<FavouriteDto>.SuccessResponse(
            new FavouriteDto
            {
                Fid = fav.Fid,
                Cid = fav.Cid,
                CurrentPrice = fav.Crypto.CurrentPrice
            },
            "Added to favourites"
        );
    }


    public async Task<ApiResponse<List<FavouriteDto>>> GetAsync(int uid)
    {
        var favs = await _repo.GetByUserAsync(uid);

        var result = favs.Select(f => new FavouriteDto
        {
            Fid = f.Fid,
            Cid = f.Cid,
            name = f.Crypto.Cname,
            symbol = f.Crypto.Csymbol,
            image = f.Crypto.CimageUrl,
            CurrentPrice = f.Crypto.CurrentPrice,
            market_cap=f.Crypto.market_cap,
            total_volume=f.Crypto.total_volume,
            price_change_24h=f.Crypto.price_change_24h,
            price_change_percentage_24h=f.Crypto.price_change_percentage_24h
        }).ToList();

        return ApiResponse<List<FavouriteDto>>
            .SuccessResponse(result, "Favourites fetched successfully");
    }

    public async Task<ApiResponse<FavouriteDto?>> GetFavIdAsync(int fid)
    {
        var fav = await _repo.GetByIdAsync(fid);

        if (fav == null)
            return null;

        var result = new FavouriteDto
        {
            Fid = fav.Fid,
            Cid = fav.Cid,
            name = fav.Crypto.Cname,
            symbol = fav.Crypto.Csymbol,
            image = fav.Crypto.CimageUrl,
            CurrentPrice = fav.Crypto.CurrentPrice,
            market_cap = fav.Crypto.market_cap,
            total_volume = fav.Crypto.total_volume,
            price_change_24h = fav.Crypto.price_change_24h,
            price_change_percentage_24h = fav.Crypto.price_change_percentage_24h
        };

        return ApiResponse<FavouriteDto?>.SuccessResponse(result, "Favourite fetched successfully");
    }


    public async Task<ApiResponse<object>> DeleteFavIdAsyn(int fid)
    {
        var fav = await _repo.GetByIdAsync(fid);

        if (fav == null)
            return null;

        await _repo.DeleteFavAsync(fav);

        return ApiResponse<object>.SuccessResponse(null, "Favourite deleted successfully");
    }
}
