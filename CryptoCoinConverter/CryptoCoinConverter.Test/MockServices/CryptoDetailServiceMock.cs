using CryptoCoinConverter.Models;
using CryptoCoinConverter.Service;
using Newtonsoft.Json;
using System.Reflection;

namespace CryptoCoinConverter.Test.MockService
{
    public class CryptoDetailServiceMock : ICryptoDetailService
    {    
        public Task<List<CryptoDetail>> GetCryptoDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<CryptoDetail>> GetCryptoDetailsCacheAsync()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestData\coincode.json");
            using StreamReader reader = new(path);
            var json = reader.ReadToEnd();
            CyptoDetailreaponse response = JsonConvert.DeserializeObject<CyptoDetailreaponse>(json);
            return Task.FromResult( response.data);
        }
    }

   
}
