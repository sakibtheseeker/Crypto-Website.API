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

    public async Task AddAsync(int uid, int cid)
    {

        var fav = new Favourite
        {
         
            Uid = uid,
            Cid = cid,
            CreatedBy = uid
        };

        await _repo.AddAsync(fav);
    }

 
    public async Task<List<FavouriteDto>> GetAsync(int uid)
    {
        var favs = await _repo.GetByUserAsync(uid);
        
        return favs.Select(f => new FavouriteDto
        {
            Fid = f.Fid,
            Cid = f.Cid,
            CryptoName = f.Crypto.Cname,
            Symbol = f.Crypto.Csymbol,
            CurrentPrice = f.Crypto.CurrentPrice
        }).ToList();

    }

    public async Task<ApiResponse<FavouriteDto?>> GetFavIdAsync(int fid)
    {

        var fav=await _repo.GetByIdAsync(fid);

        if(fav==null)
        {
            return null;
        }

        var result= new FavouriteDto
        {
            Fid = fav.Fid,
            Cid = fav.Cid,
            CryptoName = fav.Crypto.Cname,
            Symbol = fav.Crypto.Csymbol,
            CurrentPrice = fav.Crypto.CurrentPrice
        };

        return ApiResponse<FavouriteDto>.SuccessResponse(result, "Favourite Fetched Successfully");
    }

    public async Task<bool> DeleteFavIdAsyn(int fid)
    {
        var fav = await _repo.GetByIdAsync(fid);
        
        if(fav==null)
        {
            return false;
        }

        await _repo.DeleteFavAsync(fav);
        return true;

    }
}
