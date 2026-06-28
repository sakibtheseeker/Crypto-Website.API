using Crypto_Website.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.Interface
{
    public interface ICrypto
    {
        Task<List<CryptoResponseDto>> GetAll(int pageno,int pagesize,int userid);
        Task<CryptoResponseDto> Get(int id);
        public void Add(CryptoRequestDto dto);
        public void Update(CryptoRequestDto dto);
        public void Delete(int id);
    }
}
