using CryptoCoinConverter.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace CryptoCoinConverter.Service
{
    public class CryptoQuotationService : ICryptoQuotationService
    {
   
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _httpClient;
        private readonly ICryptoDetailService _cryptoDetailService;
        private readonly string _quoteUrl;
        public string[] CurrencyArray = new string[] { "USD", "EUR", "BRL", "GBP", "AUD" };

        public CryptoQuotationService( IOptions<AppSettings> settings, IHttpClientFactory httpClient, ICryptoDetailService cryptoDetailService)
        {
            _httpClient = httpClient.CreateClient("CryptoClient");
            _settings = settings;
            _cryptoDetailService = cryptoDetailService;
            _quoteUrl = $"{_settings.Value.QuoteUrl}/cryptocurrency/quotes/latest?";
        

        }
    

        public async Task<CurrencyQuotation> GetQuoteForID(string id, string currencyCode)
        {
            
            var response = await _httpClient.GetAsync(_quoteUrl + "id="+id+"&convert="+currencyCode);
            try
            {

                response.EnsureSuccessStatusCode();
                var responseResults = await response.Content.ReadAsStringAsync();
                var dynamicObject = JsonConvert.DeserializeObject<dynamic>(responseResults)!;

                var currencyQuotation = new CurrencyQuotation
                {
                    CurrencyCode = currencyCode,
                    Price = dynamicObject.data[id].quote[currencyCode].price,
                };

                return currencyQuotation;

            }
            catch (HttpRequestException ex)
            {
               
                throw;
            }
        }

        public async Task<List<CurrencyQuotation>> GetQuotesForID(string symbol)
        {

            string id = await GetMappedIdOfCrypto(symbol);
            var quotesForCrypto = await Task.WhenAll( Enumerable.Range(0, CurrencyArray.Length)
                .Select(async i => await GetQuoteForID(id, CurrencyArray[i])).ToList());

            return quotesForCrypto.ToList();
            
        }


        public async Task<string> GetMappedIdOfCrypto(string symbol)
        {

            var cryptoIds = await _cryptoDetailService.GetCryptoDetailsCacheAsync();

            var cryptoId = cryptoIds.Where(x => x.Symbol == symbol).FirstOrDefault();

            if (cryptoId == null)
                return "1";

            return cryptoId.Id.ToString();
        }
    }
}
