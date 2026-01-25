using Crypto_Website.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

//[Authorize]
[ApiController]
[Route("api/v1/favourites")]
public class FavouritesController : ControllerBase
{
    private readonly FavouriteService _service;

    public FavouritesController(FavouriteService service)
    {
        _service = service;
    }


    [HttpGet]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Client)]
    public async Task<IActionResult> Get()
    {
        int uid = int.Parse(User.FindFirst("uid")!.Value);
        return Ok(await _service.GetAsync(uid));
    }

    [HttpPost("{cryptoId}")]

    public async Task<IActionResult> Add(int cryptoId)
    {
        var uidClaim = User.FindFirst("uid");

        if (uidClaim == null)
            return Unauthorized("User ID not found in token");

        int uid = int.Parse(uidClaim.Value);

        await _service.AddAsync(uid, cryptoId);
        return Ok("Added to favourites");
    }



    [HttpGet("{fid}")]
    public async Task<IActionResult> GetById(int fid)
    {
        var fav = await _service.GetFavIdAsync(fid);
        if (fav == null)
            return NotFound("Favourite not found");

        return Ok(fav);
    }

    [HttpDelete("{fid}")]
    public async Task <IActionResult> DeleteById(int fid)
    {
        var fav = await _service.DeleteFavIdAsyn(fid);

        if (fav == null)
            return NotFound("Favourite not found");

        return Ok(new {message="Favourite Deleted Successfully"});

    }

}
