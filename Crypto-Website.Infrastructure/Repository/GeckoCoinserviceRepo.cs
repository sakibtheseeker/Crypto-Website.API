using Crypto_Website.Application.DTO;
using Crypto_Website.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Infrastructure.Repository
{
    public class GeckoCoinserviceRepo : IGeckoCoinservice
    {
        HttpClient _httpClient;
        public GeckoCoinserviceRepo(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<List<GeckoApiResponseDTO>> GetCoins()
        {
            return await _httpClient.GetFromJsonAsync<List<GeckoApiResponseDTO>>("coins/markets?vs_currency=usd");
        }
    }
}
