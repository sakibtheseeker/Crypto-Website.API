using Crypto_Website.Application.DTO.Portfolio;
using Crypto_Website.Application.Interface;

namespace Crypto_Website.Application.Services
{
    public class PortfolioService
    {
        private readonly IPortfolioRepository _repo;

        public PortfolioService(IPortfolioRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<PortfolioResponseDto>> GetPortfolioAsync(int uid)
        {
            return await _repo.GetByUserIdAsync(uid);
        }
    }
}
