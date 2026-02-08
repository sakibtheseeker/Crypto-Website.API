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
        var res = await _service.GetWalletAsync(GetUserId());

        if (res == null)
            return NotFound("Wallet not found");

        return Ok(res);
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions()
    {
        var res = await _service.GetTransactionsAsync(GetUserId());

        if (res == null)
            return NotFound("Wallet not found");

        return Ok(res);
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit(decimal amount)
    {
        var res = await _service.DepositAsync(GetUserId(), amount);

        if (res == null)
            return BadRequest("Deposit failed");

        return Ok(res);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw(decimal amount)
    {
        var res = await _service.WithdrawAsync(GetUserId(), amount);

        if (res == null)
            return BadRequest("Withdraw failed");

        return Ok(res);
    }
}
