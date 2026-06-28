using Crypto_Website.API.Helper;
using Crypto_Website.Application.DTO;
using Crypto_Website.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crypto_Website.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]

    [ApiController]
    
    public class CryptoController : ControllerBase
    {
        ICrypto Crypto;
        ICoinService coinService;
        public CryptoController(ICrypto crypto,ICoinService coinService) 
        {
            Crypto = crypto;
            this.coinService = coinService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int pageno,int pagesize)
        {
            var userid = int.Parse(User.FindFirst("uid")!.Value);
            if (userid == null)
            {
                return Unauthorized("user id not found in token");

            }
            var data=await Crypto.GetAll(pageno,pagesize,userid);
            var response =ApiResponse<List<CryptoResponseDto>>.SuccessResponse(data);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddFromApi()
        {
            await coinService.FetchAndSave();
            return Ok("data Fetch from API successfully");
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data=await Crypto.Get(id);
            var response = ApiResponse<CryptoResponseDto>.SuccessResponse(data);
            return Ok(response);
        }
    }
}
