using Crypto_Website.API.Helper;
using Crypto_Website.Application.DTO.Transaction;
using Crypto_Website.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crypto_Website.API.Controllers
{
    [Authorize]
    [Route("api/v1/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        ITransactionRepo transaction;
        public TransactionController(ITransactionRepo transaction)
        {
            this.transaction = transaction;
        }

        [HttpPost("Buy")]
        public async Task<IActionResult> Buy([FromBody] TransactionDTO dto)
        {
            var userid = int.Parse(User.FindFirst("uid")!.Value);
            if (userid == null)
            {
                return Unauthorized("user not found");
            }
            await transaction.Buy(dto,userid);
            return Ok(new { msg="Crypto added successfully"});
        }
        [HttpPost("Sell")]
        public async Task<IActionResult> Sell(TransactionDTO dto)
        {
            var userid = int.Parse(User.FindFirst("uid")!.Value);
            if (userid == null)
            {
                return Unauthorized("user not found");
            }
            await transaction.Sell(dto,userid);
            return Ok(new { msg = "Crypto sold successfully" });
        }
        [HttpGet]
        public async Task<IActionResult> Get(int transactionid)
        {
            var data = await transaction.GetById(transactionid);
            var response = ApiResponse<TransactionResponseDTO>.SuccessResponse(data);
            return Ok(response);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var userid = int.Parse(User.FindFirst("uid")!.Value);
            var data =await transaction.GetAll(userid);
            var response = ApiResponse<List<TransactionResponseDTO>>.SuccessResponse(data);
            return Ok(response);
        }
    }
}
