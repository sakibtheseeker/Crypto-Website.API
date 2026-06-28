using Crypto_Website.API.Helper;
using Crypto_Website.Application.DTO.Wallet;
using Crypto_Website.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crypto_Website.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        IWalletRepo wallet;
        public WalletController(IWalletRepo wallet)
        {
            this.wallet = wallet;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int userid)
        {
            var data=await wallet.GetBalance(userid);
            var response = ApiResponse<WalletResponse>.SuccessResponse(data);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateWallet(WalletRequest dto)
        {
            await wallet.CreateWallet(dto);
            return Ok(new { msg = "Wallet created successfully" });
        }
    }
}
