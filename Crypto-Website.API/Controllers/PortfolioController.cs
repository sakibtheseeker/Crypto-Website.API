using Crypto_Website.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;


[Authorize]
[ApiController]
[Route("api/v1/portfolio")]
public class PortfolioController : ControllerBase
{
    private readonly PortfolioService _service;

    public PortfolioController(PortfolioService service)
    {
        _service = service;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirst("uid")!.Value);
    }

    [EnableRateLimiting("fixed")]
    [HttpGet]
    public async Task<IActionResult> GetPortfolio()
    {
        var result = await _service.GetPortfolioAsync(GetUserId());
        return Ok(result);
    }

}
