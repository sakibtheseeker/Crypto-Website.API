using Crypto_Website.Application.DTO.Favourite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Interface
{
    public interface IFavourite
    {
        public Task<List<FavouriteResponseDto>> GetFavourite(int userId);
        public Task AddFavourite(int cryptoid,int userid);
        public Task DeleteFavourite(int favId);
    }
}
