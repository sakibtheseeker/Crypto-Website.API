using Crypto_Website.API.Helper;
using Crypto_Website.Application.DTO.WalletTransaction;
using Crypto_Website.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crypto_Website.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WalletTransactionController : ControllerBase
    {
        IWalletTransaction WalletTransaction;
        public WalletTransactionController(IWalletTransaction WalletTransaction)
        {
            this.WalletTransaction = WalletTransaction;
        }
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] WalletAmountRequestDTO dto)
        {
            var userid = int.Parse(User.FindFirst("uid")!.Value);
            if (userid == null)
            {
                return Unauthorized("user not found");
            }
            await WalletTransaction.Deposite(dto.Amount,userid);
            return Ok(new { msg = "amount Deposited successfully" });

        }
        [HttpPost("withdraw")]
        public async Task<IActionResult> WithDraw([FromBody]WalletAmountRequestDTO dto)
        {
            var userid = int.Parse(User.FindFirst("uid")!.Value);
            if (userid == null)
            {
                return Unauthorized("user not found");
            }
            await WalletTransaction.WithDraw(dto.Amount, userid);
            return Ok(new { msg = "amount withdraw successfully" });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var userid = int.Parse(User.FindFirst("uid")!.Value);
            if (userid == null)
            {
                return Unauthorized("user not found");
            }
            var walletTransaction=await WalletTransaction.GetWalletTransactions(userid);
            var response = ApiResponse<List<WalletTransactionResponseDTO>>.SuccessResponse(walletTransaction);
            return Ok(response);
        }


        
    }
}
