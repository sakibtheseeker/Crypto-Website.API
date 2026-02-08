using Crypto_Website.Application.DTO.Crypto;
using Crypto_Website.Application.Helper;
namespace Crypto_Website.Application.Interface
{
    public interface ICryptoService
    {
        //Task<ApiResponse<List<CryptoResponseDto>> >GetCryptosAsync();

        Task<object> GetCryptosAsync(int uid, int pageNumber, int pageSize);

        Task<ApiResponse<int> >SyncCryptosFromMarketAsync();


        Task<ApiResponse<CryptoResponseDto>>  GetCryptosByIdAsync(int cid);
    }
}
