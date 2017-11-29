using Realms;

namespace CryptoReminder.Core.CryptoCurrency.RealmContract.Dtos
{
    public class CryptoCurrencyReminderRealm : RealmObject
    {
        [PrimaryKey]
        public string MarketName { get; set; }
        public bool IsAlarmSet { get; set; }
        public double Last { get; set; }
    }
}
