using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CryptoReminder.Core.Koinex.Contract.Dto;
using Newtonsoft.Json;

namespace CryptoReminder.Core.Koinex
{
    public class KoinexDelegate : IKoinexDelegate
    {
        private HttpClient _client;

        public KoinexDelegate()
        {
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 256000;

            // _client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        public async Task<List<KoinexDto>> GetKoinexList()
        {
            try
            {
                var instruction = new Instructions();

                var path = "https://koinex.in/api/ticker";
                var uri = new Uri(string.Format(path, string.Empty));
                HttpResponseMessage response = await _client.GetAsync(uri);

                //if (response.IsSuccessStatusCode)
                //{
                //    var data = await response.Content.ReadAsStringAsync();
                //    instruction = JsonConvert.DeserializeObject<KoinexDto>(data).Instructions;
                //}

                HttpContent stream = response.Content;
                var data = stream.ReadAsStringAsync();
                string result = data.Result.ToString();

                return new List<KoinexDto>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
