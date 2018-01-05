using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;
using System.Collections.Generic;

namespace CryptoReminder.Core.RealmService
{
    public interface ICryptoRealmService
    {
        CryptoCurrencyReminderDto GetReminderDetails(CryptoCurrencyReminderDto cryptoCurrency);

        CryptoCurrencyReminderDto SaveReminder(CryptoCurrencyReminderDto cryptoCurrencyReminder);

        bool RemoveReminder(CryptoCurrencyReminderDto cryptoCurrencyReminder);

        List<CryptoCurrencyReminderDto> GetReminder(ReminderSearchDto search);
    }
}
