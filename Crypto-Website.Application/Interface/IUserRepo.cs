using Crypto_Website.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Interface
{
    public interface IUserRepo
    {
        

        public Task<List<UserResponse>> GetAllUsers();
        public Task<UserResponse> GetUsersById(int id);
        public Task UpdateUser();
        public Task DeleteUser();
    }
}
