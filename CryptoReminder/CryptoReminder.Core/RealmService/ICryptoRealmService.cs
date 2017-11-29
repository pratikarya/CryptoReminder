using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;
using System.Collections.Generic;

namespace CryptoReminder.Core.RealmService
{
    public interface ICryptoRealmService
    {
        CryptoCurrencyReminderDto GetRemider(CryptoCurrencyDto cryptoCurrency);

        void SaveReminder(CryptoCurrencyReminderDto cryptoCurrencyReminder);

        List<CryptoCurrencyReminderDto> GetReminders();
    }
}
