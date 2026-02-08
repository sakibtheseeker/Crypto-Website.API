using Crypto_Website.Application.Helper;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;

namespace Crypto_Website.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<List<User>>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            return ApiResponse<List<User>>
                .SuccessResponse(users, "Users fetched successfully");
        }

        public async Task<ApiResponse<User?>> GetByIdAsync(int uid)
        {
            var user = await _repository.GetByIdAsync(uid);

            if (user == null)
                return null;

            return ApiResponse<User?>
                .SuccessResponse(user, "User fetched successfully");
        }

        public async Task<ApiResponse<object>> DeleteUserAsync(int uid)
        {
            var user = await _repository.GetByIdAsync(uid);

            if (user == null)
                return null;

            await _repository.DeleteAsync(user);

            return ApiResponse<object>
                .SuccessResponse(null, "User deleted successfully");
        }
    }
}
