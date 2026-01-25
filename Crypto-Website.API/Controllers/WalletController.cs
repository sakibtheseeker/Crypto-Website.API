using Crypto_Website.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/v1/wallet")]
public class WalletController : ControllerBase
{
    private readonly WalletService _service;

    public WalletController(WalletService service)
    {
        _service = service;
    }
    private int GetUserId()
    {
        var uidClaim =
            User.FindFirst("uid") ??
            User.FindFirst(ClaimTypes.NameIdentifier);

        if (uidClaim == null)
            throw new UnauthorizedAccessException("User ID not found in token");

        return int.Parse(uidClaim.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetWallet()
    {
        return Ok(await _service.GetWalletAsync(GetUserId()));
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions()
    {
        return Ok(await _service.GetTransactionsAsync(GetUserId()));
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit(decimal amount)
    {
        await _service.DepositAsync(GetUserId(), amount);
        return Ok("Deposit successful");
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw(decimal amount)
    {
        await _service.WithdrawAsync(GetUserId(), amount);
        return Ok("Withdraw successful");
    }
}
