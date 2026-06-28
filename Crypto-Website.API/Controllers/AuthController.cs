using Crypto_Website.API.Helper;
using Crypto_Website.Application.DTO;
using Crypto_Website.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crypto_Website.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuth auth;
        public AuthController(IAuth auth)
        {
            this.auth = auth;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            await auth.RegisterUser(dto);
            return Ok("User Register Successfully");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var data = await auth.LoginUser(dto);
            var response = ApiResponse<LoginResponseDTO>.SuccessResponse(data);
            return Ok(response);
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenRequest token)
        {
            var data = await auth.ValidateRefreshToken(token.Token);
            var response = ApiResponse<LoginResponseDTO>.SuccessResponse(data);
            return Ok(response);
        }
    }
}
