using CryptoCoinConverter.Models;

namespace CryptoCoinConverter.Service
{
    public interface ICryptoDetailService
    {
        Task<List<CryptoDetail>> GetCryptoDetailsCacheAsync();
        Task<List<CryptoDetail>> GetCryptoDetailsAsync();
    }
}
