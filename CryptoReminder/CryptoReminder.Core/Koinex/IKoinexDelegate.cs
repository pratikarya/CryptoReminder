using CryptoReminder.Core.Koinex.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoReminder.Core.Koinex
{
    public interface IKoinexDelegate
    {
        Task<List<KoinexDto>> GetKoinexList();

        //Task<List<CryptoCurrencyReminderDto>> GetMyCryptoCurrencyList();
    }
}
