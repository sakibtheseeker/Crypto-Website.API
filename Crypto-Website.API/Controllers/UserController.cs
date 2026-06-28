using Crypto_Website.API.Helper;
using Crypto_Website.Application.DTO;
using Crypto_Website.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Crypto_Website.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepo user;
        public UserController(IUserRepo user)
        {
            this.user = user;
        }
        

        [HttpGet("users")]
        public async Task<IActionResult> GetAll()
        {
            var users=await user.GetAllUsers();
            var response = ApiResponse<List<UserResponse>>.SuccessResponse(users);
            return Ok(response);
        }
        [HttpGet("userById")]
        public async Task<IActionResult> GetById(int id)
        {
            var u = await user.GetUsersById(id);
            var response = ApiResponse<UserResponse>.SuccessResponse(u);
            return Ok(response);
        }
    }
}
