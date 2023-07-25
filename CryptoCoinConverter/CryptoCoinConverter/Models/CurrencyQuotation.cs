using System.Collections;

namespace CryptoCoinConverter.Models
{
    public class CurrencyQuotation
    {
        public string CurrencyCode { get; set; } = string.Empty;        
        public double Price { get; set; }
    }
}
