using Crypto_Website.API.Helper;
using Crypto_Website.Application.DTO.Portfolio;
using Crypto_Website.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Crypto_Website.API.Controllers
{
    [Authorize]
    [Route("api/v1/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        IPortfolio portfolio;
        public PortfolioController(IPortfolio portfolio)
        {
            this.portfolio = portfolio;
        }
        [HttpGet("PortfolioGet")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> GetPorfolio(int userId)
        {
            var data = await portfolio.GetPortfolio(userId);
            var response = ApiResponse<PortfolioDTO>.SuccessResponse(data);
            return Ok(response);
        }
        [HttpGet("PortfolioAssetsGet")]
        [ResponseCache(Duration =10,Location =ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetPortfolioAssets()
        {
            var userid = int.Parse(User.FindFirst("uid")!.Value);
            var data = await portfolio.GetPortfolioAsset(userid);
            var response = ApiResponse<List<PortfolioAssetDTO>>.SuccessResponse(data);
            return Ok(response);
        }
        [HttpGet("GetPortfolioAssetsStat")]
        public async Task<IActionResult> GetPortfolioStat()
        {
            var userid = int.Parse(User.FindFirst("uid")!.Value);
            var data = await portfolio.GetPortfolioAssetsStat(userid);
            var response = ApiResponse<PortfolioAssetsStatDTO>.SuccessResponse(data);
            return Ok(response);
        }

    }
}
