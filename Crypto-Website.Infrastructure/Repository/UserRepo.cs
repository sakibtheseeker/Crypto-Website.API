using AutoMapper;
using Crypto_Website.Application.DTO;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Infrastructure.Repository
{
    public class UserRepo : IUserRepo
    {
        IMapper mapper;
        ApplicationDbContext _context;
        public UserRepo(ApplicationDbContext context,IMapper mapper)
        {
            this.mapper = mapper;
            this._context = context;
        }

        public Task DeleteUser()
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            var data= await _context.Users.ToListAsync();
            var users = mapper.Map<List<UserResponse>>(data);
            return users;
        }

        public async Task<UserResponse> GetUsersById(int id)
        {
            var data = await _context.Users.FindAsync(id);
            var user = mapper.Map<UserResponse>(data);
            return user;

        }

        

        public Task UpdateUser()
        {
            throw new NotImplementedException();
        }



        //public async Task UpdateUser(UserResponse dto)
        //{
        //    var data = mapper.Map<User>(dto);
        //    await _context.Users.update;
        //}
    }
}
