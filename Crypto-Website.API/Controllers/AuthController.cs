using Crypto_Website.Application.DTO.Auth;
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

        public AuthController(IUserRepository userRepo, JwtService jwtService, RegisterService service)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);

            if (user == null || user.Upassword != dto.Password)
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {   user.Uname,
                user.Uemail,
                token
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Add(RegisterDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("User created");
        }
    }
}
