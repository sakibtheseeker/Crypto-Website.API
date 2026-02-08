using Crypto_Website.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Crypto_Website.Logging;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/v1/crypto")]
public class CryptoController : ControllerBase
{
    private readonly ICryptoService _cryptoService;

    public CryptoController(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncCryptos()
    {
        var count = await _cryptoService.SyncCryptosFromMarketAsync();

        await AppLogger.Log(
            $"Crypto sync completed. Inserted count: {count}",
            LogLevelType.Information);

        return Ok(count);
    }

    [HttpGet]
    public async Task<IActionResult> GetCryptos(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        int uid = int.Parse(User.FindFirst("uid")!.Value);
        var result = await _cryptoService.GetCryptosAsync(uid,pageNumber, pageSize);
        return Ok(result);
    }

    [HttpGet("{cid}")]
    public async Task<IActionResult> GetCrytoById(int cid)
    {

        var cry = await _cryptoService.GetCryptosByIdAsync(cid);

        if (cry == null)
        {
            return NotFound("Crypto not found");
        }

        return Ok(cry);
    }
}
