using Newtonsoft.Json;
using System.Collections.Generic;

namespace CryptoReminder.Core.CryptoCurrency.Contract.Dtos
{
    public class CryptoCurrencyResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public List<CryptoCurrencyDto> Currencies { get; set; }
    }
}
