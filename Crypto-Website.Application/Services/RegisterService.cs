using Crypto_Website.Application.DTO.Auth;
using Crypto_Website.Application.Interface;
using Crypto_Website.Domain.Models;

namespace Crypto_Website.Application.Services
{
    public class RegisterService
    {
        private readonly IRegisterRepository _repository;
        private readonly IWalletRepository _walletRepo;

        public RegisterService(
            IRegisterRepository repository,
            IWalletRepository walletRepo)
        {
            _repository = repository;
            _walletRepo = walletRepo;
        }

        public async Task AddAsync(RegisterDto dto)
        {
            // 1️⃣ Create User
            var user = new User
            {
                Uname = dto.Uname,
                Uemail = dto.Uemail,
                Upassword = dto.Upassword,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.AddAsync(user);

            // 2️⃣ Create Wallet for User (CRITICAL)
            var wallet = new Wallet
            {
                Uid = user.Uid,
                CurrentBal = 0,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = user.Uid,
                IsActive = true
            };

            await _walletRepo.AddAsync(wallet);
        }
    }
}
