﻿using System;
using System.Collections.Generic;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using CryptoReminder.Core.RealmService;
using MvvmCross.Platform;

namespace CryptoReminder.Core.CryptoCurrency
{
    public class CryptoDelegate : ICryptoDelegate
    {
        private HttpClient _client;
        public ICryptoRealmService RealmService;

        public CryptoDelegate()
        {
            RealmService = Mvx.Resolve<ICryptoRealmService>();

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<CryptoCurrencyDto>> GetCryptoCurrencyList()
        {
            try
            {
                List<CryptoCurrencyDto> CurrencyList = new List<CryptoCurrencyDto>();

                var path = "https://bittrex.com/api/v1.1/public/getmarketsummaries";
                HttpResponseMessage response = await _client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    CurrencyList = JsonConvert.DeserializeObject<CryptoCurrencyResponse>(data).Currencies;
                }

                return CurrencyList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CryptoCurrencyReminderDto>> GetReminder(ReminderSearchDto search)
        {
            return await Task.Run(() => {
                var response = RealmService.GetReminder(search);
                return response;
            });
        }

        public async Task<CryptoCurrencyReminderDto> SaveReminder(CryptoCurrencyReminderDto cryptoCurrencyReminder)
        {
            return await Task.Run(() =>
            {
                CryptoCurrencyReminderDto res;
                res = RealmService.SaveReminder(cryptoCurrencyReminder);
                return res;
            });
        }

        public async Task<bool> RemoveReminder(CryptoCurrencyReminderDto cryptoCurrencyReminder)
        {
            return await Task.Run(() =>
            {
                bool res;
                res = RealmService.RemoveReminder(cryptoCurrencyReminder);
                return res;
            });
        }

        public async Task<CryptoCurrencyReminderDto> GetReminderDetails(CryptoCurrencyReminderDto cryptoCurrency)
        {
            return await Task.Run(() =>
            {
                CryptoCurrencyReminderDto res;
                res = RealmService.GetReminderDetails(cryptoCurrency);
                return res;
            });
        }
    }
}
