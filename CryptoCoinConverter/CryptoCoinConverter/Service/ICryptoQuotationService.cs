using CryptoCoinConverter.Models;

namespace CryptoCoinConverter.Service
{
    public interface ICryptoQuotationService
    {
          Task <CurrencyQuotation> GetQuoteForID(string id, string currencyCode);
          Task<List<CurrencyQuotation>> GetQuotesForID(string symbol);
    }
}
