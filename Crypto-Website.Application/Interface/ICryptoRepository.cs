using Crypto_Website.Domain.Models;

namespace Crypto_Website.Application.Interface
{
    public interface ICryptoRepository
    {
        Task<List<Crypto>> GetAllAsync();

        Task<(List<Crypto> Data, int TotalRecords)>GetPagedAsync(int pageNumber, int pageSize);

        Task<Crypto> GetByIdAsync(int cid);
        Task<bool> ExistsAsync(string symbol);
        Task<bool> ExistsByIdAsync(int cid);
        Task AddAsync(Crypto crypto);
    }
}
