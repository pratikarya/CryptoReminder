namespace CryptoReminder.Core.CryptoCurrency.Contract.Dtos
{
    public class CryptoCurrencyReminderDto
    {
        public string MarketName { get; set; }
        public double Last { get; set; }
        public bool IsAlarmSet { get; set; }
    }
}
