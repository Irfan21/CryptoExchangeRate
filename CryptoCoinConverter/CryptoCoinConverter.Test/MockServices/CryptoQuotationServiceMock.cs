using CryptoCoinConverter.Models;
using CryptoCoinConverter.Service;
using Newtonsoft.Json;
using System.Reflection;

namespace CryptoCoinConverter.Test.MockService
{
    public class CryptoQuotationServiceMock : ICryptoQuotationService
    {
        private readonly ICryptoDetailService _cryptoDetailService;
        public string[] CurrencyArray = new string[] { "USD", "EUR", "BRL", "GBP", "AUD" };
        public CryptoQuotationServiceMock(ICryptoDetailService cryptoDetailService)
        {
            _cryptoDetailService = cryptoDetailService;
        }
        public Task<CurrencyQuotation> GetQuoteForID(string id, string currencyCode)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CurrencyQuotation>> GetQuotesForID(string symbol)
        {
            string id = await GetMappedIdOfCrypto(symbol);
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestData\prodata.json");
            using StreamReader reader = new(path);
            var json = reader.ReadToEnd();
            var dynamicObject = JsonConvert.DeserializeObject<dynamic>(json)!;

            var quotesForCrypto = Enumerable.Range(0, CurrencyArray.Length)
           .Select(i => new CurrencyQuotation
           {
               CurrencyCode = CurrencyArray[i],
               Price = dynamicObject.data[id].quote[CurrencyArray[i]].price,
           }).ToList();

            return quotesForCrypto.ToList();
        }

        public async Task<string> GetMappedIdOfCrypto(string symbol)
        {

            var cryptoIds = await _cryptoDetailService.GetCryptoDetailsCacheAsync();

            if (cryptoIds == null)
                return "BTC";

            var cryptoId = cryptoIds.Where(x => x.Symbol == symbol).FirstOrDefault();

            return cryptoId.Id.ToString();
        }
    }
}
