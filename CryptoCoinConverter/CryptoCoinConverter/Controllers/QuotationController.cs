using CryptoCoinConverter.Service;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCoinConverter.Controllers
{
    public class QuotationController : Controller
    {
        private readonly ICryptoQuotationService _quotationService;

        public QuotationController(ICryptoQuotationService quotationService)
        {
            _quotationService = quotationService;
        }

        
        [Route("{controller}/{id=BTC}")]
        public async Task<IActionResult> CryptoQuotesView(string id)
        {
            ViewBag.Id = id;
            try
            {
                var currencyQuote = await _quotationService.GetQuotesForID(id);
                return View(currencyQuote.AsEnumerable());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"Failed because of following error ({ex.GetType().Name} - {ex.Message})");
                return View("/Error",ex);
            }

           
        }
        
       

    }
}
