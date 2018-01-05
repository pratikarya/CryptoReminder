using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoReminder.Core.CryptoCurrency.Contract.Dtos
{
    public class ReminderSearchDto
    {
        public CryptoCurrencyDto cryptoCurrency { get; set; }
        public SearchType Type { get; set; }
    }

    public enum SearchType
    {
        AllReminders,
        GroupedReminders,
        AllRemindersFor1Coin
    }
}
