using Crypto_Website.Application.DTO.Crypto;
using Crypto_Website.Application.Helper;
namespace Crypto_Website.Application.Interface
{
    public interface ICryptoService
    {
        Task<List<CryptoResponseDto>> GetCryptosAsync();

        Task<object> GetCryptosAsync(int pageNumber, int pageSize);

        Task<int> SyncCryptosFromMarketAsync();


        Task<ApiResponse<CryptoResponseDto>>  GetCryptosByIdAsync(int cid);
    }
}
