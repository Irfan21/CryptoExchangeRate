using CryptoCoinConverter.Controllers;
using CryptoCoinConverter.Models;
using CryptoCoinConverter.Service;
using Microsoft.AspNetCore.Mvc;
using Mockservices = CryptoCoinConverter.Test.MockService;

namespace CryptoCoinConverter.PresentationLayer.Test
{
    [TestClass]
    public class QuotationControllerTest
    {

        ICryptoDetailService cryptoDetailService;
        ICryptoQuotationService cryptoQuotationService;
        public QuotationControllerTest()
        {
            cryptoDetailService = new Mockservices.CryptoDetailServiceMock();
            cryptoQuotationService = new Mockservices.CryptoQuotationServiceMock(cryptoDetailService);

        }


        [TestMethod]
        public async Task CryptoQuotesView_ForCryptoCode_returnListViewofQuotation()
        {
          
            var quotationController = new QuotationController(cryptoQuotationService);

            var result = await quotationController.CryptoQuotesView("BTC") as ViewResult;
            var model = result.Model as List<CurrencyQuotation>;


            Assert.IsNotNull(model);
            CollectionAssert.AllItemsAreInstancesOfType(model, typeof(CurrencyQuotation));
            CollectionAssert.AllItemsAreUnique(model);

        }
    }
}