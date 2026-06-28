using Crypto_Website.API.Helper;
using Crypto_Website.Application.DTO.Favourite;
using Crypto_Website.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crypto_Website.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        IFavourite fav;
        public FavouriteController(IFavourite fav)
        {
            this.fav = fav;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userid = int.Parse(User.FindFirst("uid")!.Value);
            if (userid == null)
            {
                return Unauthorized("user id not found in token");

            }
            var data= await fav.GetFavourite(userid);
            var response = ApiResponse<List<FavouriteResponseDto>>.SuccessResponse(data);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromQuery] int cryptoid)
        {
            var userid = int.Parse(User.FindFirst("uid")!.Value);
            if (userid == null)
            {
                return Unauthorized("user id not found in token");

            }
            await fav.AddFavourite(cryptoid,userid);
            return Ok("Added to favourite");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int favId)
        {
            var userid = int.Parse(User.FindFirst("uid")!.Value);
            if (userid == null)
            {
                return Unauthorized("user id not found in token");

            }
            await fav.DeleteFavourite(favId);
            return Ok("favouriteDeleted");
        }
    }
}
