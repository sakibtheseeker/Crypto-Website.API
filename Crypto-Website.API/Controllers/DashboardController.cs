using Crypto_Website.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crypto_Website.API.Controllers
{
    [Authorize]
    [Route("api/v1/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _service;
        public DashboardController(DashboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var uidClaim = User.Claims.FirstOrDefault(c => c.Type == "uid");

            if (uidClaim == null)
                return Unauthorized("User not authenticated");

            int uid = int.Parse(uidClaim.Value);
            return Ok(await _service.GetDashboardAsync(uid));
        }
    }
}
