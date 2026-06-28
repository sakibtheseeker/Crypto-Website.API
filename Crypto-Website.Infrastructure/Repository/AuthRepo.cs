using AutoMapper;
using Crypto_Website.Application.DTO;
using Crypto_Website.Application.DTO.Portfolio;
using Crypto_Website.Application.Exceptions;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Infrastructure.Repository
{
    public class AuthRepo:IAuth
    {
        private readonly ApplicationDbContext _context;
        IMapper mapper;
        IConfiguration config;
        public AuthRepo(IMapper mapper,ApplicationDbContext context,IConfiguration config)
        {
            this._context = context;
            this.mapper = mapper;
            this.config = config;
        }
        public async Task<LoginResponseDTO> LoginUser(LoginDTO dto)
        {
            var data = await _context.Users.FirstOrDefaultAsync(X => X.UserEmail == dto.Email);

            if (data == null)
                throw new NotFoundException("user not found");
            if ( data.UserPassword != dto.Password)
            {
                throw new UnauthorizedException("Invalid Credentails");
            }
                var refreshtoken = GenerateRefreshToken();
                var refreshtokendto = new RefreshTokenDTO
                {
                    UserId = data.Userid,
                    Token = refreshtoken,
                    IsRevoked = false,
                    ExpireTime = DateTime.UtcNow.AddDays(7),
                };
                var mappedrefreshtokendto = mapper.Map<RefreshToken>(refreshtokendto);
                await _context.RefreshTokens.AddAsync(mappedrefreshtokendto);
                await _context.SaveChangesAsync();
                var token = Generatetoken(data);
                var user = new LoginResponseDTO
                {
                    UserEmail = dto.Email,
                    token = token,
                    ExpireTime=config["jwt:ExpireMinutes"].ToString(),
                    RefreshToken=refreshtoken
                };
                return user;
            
        }

        public async Task RegisterUser(RegisterDTO dto)
        {
            var data = mapper.Map<User>(dto);

            await _context.Users.AddAsync(data);
            await _context.SaveChangesAsync();
            var portfolio = new PortfolioDTO
            {
                Userid = data.Userid,
                IsActive=true,
                CreatedBy = data.Userid,
                CreatedAt = DateTime.UtcNow
            };
            var mapportfolio = mapper.Map<Portfolio>(portfolio);
            await _context.Portfolios.AddAsync(mapportfolio);
            await _context.SaveChangesAsync();
        }

        public string Generatetoken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Userid.ToString()),
                new Claim("uid",user.Userid.ToString()),
                new Claim(ClaimTypes.Email,user.UserEmail)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwt:Key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                issuer: config["jwt:Issuer"],
                audience: config["jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(config["jwt:ExpireMinutes"])),
                signingCredentials:cred

            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        }
        public async Task<LoginResponseDTO> ValidateRefreshToken(string token)
        {
            var storedToken = await _context.RefreshTokens.Include(x=>x.User).FirstOrDefaultAsync(e => e.Token == token);
            if (storedToken == null || storedToken.IsRevoked || storedToken.ExpireTime<DateTime.UtcNow)
            {
                return null;
            }
            var user = storedToken.User;
            storedToken.IsRevoked = true;
            await _context.SaveChangesAsync();
            var newRefreshToken = GenerateRefreshToken();
            var data = new RefreshTokenDTO
            {
                UserId = user.Userid,
                Token = newRefreshToken,
                IsRevoked = false,
                ExpireTime = DateTime.UtcNow.AddDays(7),
            };
            var refreshtoken = mapper.Map<RefreshToken>(data);
            await _context.RefreshTokens.AddAsync(refreshtoken);
            await _context.SaveChangesAsync();
            var accesstoken = Generatetoken(user);
            var loginresponse = new LoginResponseDTO()
            {
                UserEmail = user.UserEmail,
                token = accesstoken,
                RefreshToken = newRefreshToken,
                ExpireTime = config["jwt:ExpireMinutes"]

            };
            return loginresponse;
        }
    }
    


    }
