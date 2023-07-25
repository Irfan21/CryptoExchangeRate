using CryptoCoinConverter.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;


namespace CryptoCoinConverter.Service
{
    public class CryptoDetailService : ICryptoDetailService
    {
       
        private readonly IOptions<AppSettings> _settings;
        private readonly IMemoryCache _memoryCache;
        public const string BitCoinKey = "BITCOINCACHEKEY";
        private readonly HttpClient _httpClient;
        private readonly string _mapUrl;
      
        public CryptoDetailService(IOptions<AppSettings> settings, IHttpClientFactory httpClient, IMemoryCache memoryCache)
        {
         
            _settings = settings;
            _httpClient = httpClient.CreateClient("CryptoClient");           
            _memoryCache = memoryCache;
            _mapUrl = $"{_settings.Value.CryptoMapUrl}/cryptocurrency/map";
       
        }
        public async Task<List<CryptoDetail>> GetCryptoDetailsCacheAsync()
        {
        
            var cryptoDetailList = await _memoryCache.GetOrCreateAsync<List<CryptoDetail>>(BitCoinKey, async cache =>
            {
                cache.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
                return await GetCryptoDetailsAsync();
            });
            return cryptoDetailList;
        }
        public async Task<List<CryptoDetail>> GetCryptoDetailsAsync()
        {
           
            var response = await _httpClient.GetAsync(_mapUrl);
            try
            {

                response.EnsureSuccessStatusCode();
                var responseResults = await response.Content.ReadFromJsonAsync<CyptoDetailreaponse>();
                var cryptodetail = responseResults.data;
                return cryptodetail;
               
            }
            catch (HttpRequestException ex)
            {            
                throw;
            }            
        }
    }
}
