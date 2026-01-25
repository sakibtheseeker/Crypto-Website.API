using Crypto_Website.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crypto_Website.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

 
        [HttpGet("{uid}")]
        public async Task<IActionResult> GetById(int uid)
        {
            var user = await _service.GetByIdAsync(uid);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteUserAsync(id);

            if (!result)
                return NotFound("User not found");

            return Ok(new { message = "User deleted successfully" });
        }
    }
}
