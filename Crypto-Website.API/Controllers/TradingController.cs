using Crypto_Website.Application.DTO.Trading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/v1/trade")]
public class TradingController : ControllerBase
{
    private readonly TradingService _service;

    public TradingController(TradingService service)
    {
        _service = service;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirst("uid")!.Value);
    }

    [HttpPost("buy")]
    public async Task<IActionResult> Buy(BuySellDto dto)
    {
        await _service.BuyAsync(GetUserId(), dto);
        return Ok("Buy order successful");
    }

    [HttpPost("sell")]
    public async Task<IActionResult> Sell(BuySellDto dto)
    {
        await _service.SellAsync(GetUserId(), dto);
        return Ok("Sell order successful");
    }


    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions()
    {
        return Ok(await _service.GetTransactionsAsync(GetUserId()));
    }
}
