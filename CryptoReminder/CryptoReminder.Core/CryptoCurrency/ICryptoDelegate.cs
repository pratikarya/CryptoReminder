using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoReminder.Core.CryptoCurrency
{
    public interface ICryptoDelegate
    {
        Task<List<CryptoCurrencyDto>> GetCryptoCurrencyList();

        Task<List<CryptoCurrencyReminderDto>> GetMyCryptoCurrencyList();
    }
}
