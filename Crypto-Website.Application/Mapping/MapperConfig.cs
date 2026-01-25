using AutoMapper;
using Crypto_Website.Domain.Models;
using Crypto_Website.Application.DTO.Transaction;
using Crypto_Website.Application.DTO.Portfolio;
using Crypto_Website.Application.DTO.Wallet;

namespace Crypto_Website.Application.Mapping
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
           CreateMap<WalletTransaction, WalletTransactionResponseDto>();
        }
    }
}
