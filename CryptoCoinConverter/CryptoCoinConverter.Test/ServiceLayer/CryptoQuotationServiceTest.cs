
using CryptoCoinConverter.Models;
using CryptoCoinConverter.Service;
using Mockservices = CryptoCoinConverter.Test.MockService;


namespace CryptoCoinConverter.Test.ServiceLayer
{
    [TestClass]
    public class CryptoQuotationServiceTest
    {
  
        ICryptoDetailService cryptoDetailService;
        Mockservices.CryptoQuotationServiceMock currencyQuotationService;
        public CryptoQuotationServiceTest()
        {
            cryptoDetailService = new Mockservices.CryptoDetailServiceMock();
            currencyQuotationService = new Mockservices.CryptoQuotationServiceMock(cryptoDetailService);
        }

        [TestMethod]
        public async Task GetQuotesForID_For_CryptoCode_ReturnesQuotesInAllCurrencies()
        {

            var result = await currencyQuotationService.GetQuotesForID("BTC");
            var models = result.ToList();

            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(models, typeof(CurrencyQuotation));
            CollectionAssert.AllItemsAreUnique(models);

        }

        [TestMethod]
        public async Task GetMappedIdOfCrypto_For_CryptoCode_ReturnesMapedID()
        {
         
            var result = await currencyQuotationService.GetMappedIdOfCrypto("BTC");
            
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "1");
        }
      



    }
}
