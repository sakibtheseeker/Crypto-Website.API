using Crypto_Website.Domain.Models;
using Crypto_Website.Application.DTO;
namespace Crypto_Website.Application.Interface
{
    public interface ICryptoRepository
    {
        Task<List<Crypto>> GetAllAsync();

        Task<Crypto> GetByIdAsync(int cid);
        Task<bool> ExistsAsync(string symbol);
        Task<bool> ExistsByIdAsync(int cid);
        Task AddAsync(Crypto crypto);

        Task<Crypto?> GetBySymbolAsync(string symbol);
        Task UpdateAsync(Crypto crypto);

        Task<(List<Crypto> Data, int TotalRecords)> GetPagedAsync(int pageNumber, int pageSize);

        Task<List<UserHoldingDTO>> GetUserPortfolioAssetsAsync(int uid);
    }

}
