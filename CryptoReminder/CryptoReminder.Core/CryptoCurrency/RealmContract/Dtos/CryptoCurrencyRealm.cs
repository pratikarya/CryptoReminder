using Realms;
using System;

namespace CryptoReminder.Core.CryptoCurrency.RealmContract.Dtos
{
    public class CryptoCurrencyRealm : RealmObject
    {
        [PrimaryKey]
        public string MarketName { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Volume { get; set; }
        public double Last { get; set; }
        public double BaseVolume { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public int OpenBuyOrders { get; set; }
        public int OpenSellOrders { get; set; }
        public double PrevDay { get; set; }
        public DateTimeOffset Created { get; set; }

    }
}
