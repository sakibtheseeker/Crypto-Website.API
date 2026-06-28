using AutoMapper;
using Crypto_Website.Application.DTO;
using Crypto_Website.Application.DTO.Favourite;
using Crypto_Website.Application.DTO.Portfolio;
using Crypto_Website.Application.DTO.Transaction;
using Crypto_Website.Application.DTO.Wallet;
using Crypto_Website.Application.DTO.WalletTransaction;
using Crypto_Website.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Mapping
{
    public class MapperConfig:Profile
    {
        public MapperConfig() {
            CreateMap<Crypto, CryptoRequestDto>().ReverseMap();
            CreateMap<Crypto, CryptoResponseDto>().ReverseMap();
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<Favorites,FavouriteRequestDto>().ReverseMap();
            CreateMap<Favorites, FavouriteResponseDto>()
                .ForMember(dest => dest.CryptoName, opt => opt.MapFrom(x => x.Crypto.CryptoName))
                .ForMember(dest=>dest.CryptoImage,opt=>opt.MapFrom(x=>x.Crypto.CryptoImage))
                .ForMember(dest=>dest.CryptoSymbol,opt=>opt.MapFrom(x=>x.Crypto.CryptoSymbol))
                .ForMember(dest=>dest.CurrentPrice,opt=>opt.MapFrom(x=>x.Crypto.CurrentPrice))
                .ForMember(dest=>dest.MarketCap,opt=>opt.MapFrom(x=>x.Crypto.MarketCap))
                .ForMember(dest=>dest.TotalVolume,opt=>opt.MapFrom(x=>x.Crypto.TotalVolume))
                .ForMember(dest=>dest.PriceChange24h,opt=>opt.MapFrom(x=>x.Crypto.PriceChange24h))
                .ForMember(dest=>dest.PriceChangepercentage24h,opt=>opt.MapFrom(x=>x.Crypto.PriceChangepercentage24h))
                .ReverseMap()
                .ForMember(dest => dest.Crypto, opt => opt.Ignore());
            CreateMap<Transaction, TransactionBuyDTO>().ReverseMap();
            CreateMap<Transaction, TransactionSellDTO>().ReverseMap();
            CreateMap<Transaction,TransactionResponseDTO>()
                .ForMember(dest=>dest.UserName,opt=>opt.MapFrom(x=>x.User.UserName))
                .ForMember(dest=>dest.CryptoName,opt=>opt.MapFrom(x=>x.Crypto.CryptoName))
                .ForMember(dest=>dest.CryptoSymbol,opt=>opt.MapFrom(x=>x.Crypto.CryptoSymbol))
                .ReverseMap()
                .ForMember(dest=>dest.User,opt=>opt.Ignore())
                .ForMember(dest=>dest.Crypto,opt=>opt.Ignore());
            CreateMap<Wallet, WalletResponse>().ReverseMap();
            CreateMap<Wallet, WalletRequest>().ReverseMap();
            CreateMap<WalletTransaction, WalletTransactionRequestDTO>().ReverseMap();
            CreateMap<WalletTransaction, WalletTransactionResponseDTO>().ReverseMap();
            CreateMap<Portfolio, PortfolioDTO>().ReverseMap();
            CreateMap<PortfolioAsset, PortfolioAssetDTO>()
                .ForMember(dest=>dest.CryptoName,opt=>opt.MapFrom(x=>x.Crypto.CryptoName))
                .ForMember(dest => dest.CryptoImage, opt => opt.MapFrom(x => x.Crypto.CryptoImage))
                .ForMember(dest => dest.CryptoSymbol, opt => opt.MapFrom(x => x.Crypto.CryptoSymbol))
                .ForMember(dest => dest.CurrentPrice, opt => opt.MapFrom(x => x.Crypto.CurrentPrice))
                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(x => x.Crypto.ProviderName))

                .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(x => x.Crypto.MarketCap))
                .ForMember(dest => dest.TotalVolume, opt => opt.MapFrom(x => x.Crypto.TotalVolume))
                .ForMember(dest => dest.PriceChange24h, opt => opt.MapFrom(x => x.Crypto.PriceChange24h))
                .ForMember(dest => dest.PriceChangepercentage24h, opt => opt.MapFrom(x => x.Crypto.PriceChangepercentage24h))
                .ReverseMap()
                .ForMember(dest=>dest.Crypto,opt=>opt.Ignore());
            CreateMap<RefreshToken, RefreshTokenDTO>().ReverseMap();
        }
    }
}
