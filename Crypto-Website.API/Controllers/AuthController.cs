using Crypto_Website.Application.DTO.Auth;
using Crypto_Website.Application.Helper;
using Crypto_Website.Application.Interface;
using Crypto_Website.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crypto_Website.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtService _jwtService;
        private readonly RegisterService _service;
        private readonly IRefreshTokenRepository _refreshTokenRepo;


        public AuthController(IUserRepository userRepo, JwtService jwtService, RegisterService service, IRefreshTokenRepository refreshTokenRepo)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
            _service = service;;;;;
            _refreshTokenRepo = refreshTokenRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);

            if (user == null || user.Upassword != dto.Password)
                return Unauthorized("Invalid credentials");

            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            await _refreshTokenRepo.SaveAsync(
                user.Uid,
                refreshToken,
                DateTime.UtcNow.AddDays(7)
            );

            return Ok(new
            {
                user.Uname,
                user.Uemail,
                accessToken,
                refreshToken
            });
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(
    RefreshTokenRequestDto dto)
        {
            var storedToken = await _refreshTokenRepo.GetAsync(dto.RefreshToken);

            if (storedToken == null ||
                !storedToken.IsActive ||
                storedToken.ExpiresAt < DateTime.UtcNow)
            {
                return Unauthorized("Invalid refresh token");
            }

         
            await _refreshTokenRepo.RevokeAsync(storedToken);

            var user = await _userRepo.GetByIdAsync(storedToken.Uid);

            if (user == null)
                return Unauthorized("User not found");


            var newAccessToken = _jwtService.GenerateAccessToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            await _refreshTokenRepo.SaveAsync(
                user.Uid,
                newRefreshToken,
                DateTime.UtcNow.AddDays(7)
            );

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }



        [HttpPost("register")]
        public async Task<IActionResult> Add(RegisterDto dto)
        {
            await _service.AddAsync(dto);

            return Ok(
                ApiResponse<object>.SuccessResponse(
                    null,
                    "User created"
                )
            );
        }

    }
}
