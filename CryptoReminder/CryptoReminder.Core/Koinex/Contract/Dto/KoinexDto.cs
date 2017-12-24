using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoReminder.Core.Koinex.Contract.Dto
{
    public class KoinexDto
    {
        [JsonProperty("greeting")]
        public string Greeting { get; set; }

        [JsonProperty("instructions")]
        public Instructions Instructions { get; set; }
    }

    public class Instructions
    {
        [JsonProperty("prices")]
        public Prices Prices { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }
    }

    public class Stats
    {
        [JsonProperty("ETH")]
        public Bch Eth { get; set; }

        [JsonProperty("BTC")]
        public Bch Btc { get; set; }

        [JsonProperty("LTC")]
        public Bch Ltc { get; set; }

        [JsonProperty("XRP")]
        public Bch Xrp { get; set; }

        [JsonProperty("BCH")]
        public Bch Bch { get; set; }
    }

    public class Bch
    {
        [JsonProperty("last_traded_price")]
        public string LastTradedPrice { get; set; }

        [JsonProperty("lowest_ask")]
        public string LowestAsk { get; set; }

        [JsonProperty("highest_bid")]
        public string HighestBid { get; set; }

        [JsonProperty("min_24hrs")]
        public string Min24Hrs { get; set; }

        [JsonProperty("max_24hrs")]
        public string Max24Hrs { get; set; }

        [JsonProperty("vol_24hrs")]
        public string Vol24Hrs { get; set; }
    }

    public class Prices
    {
        [JsonProperty("BTC")]
        public string Btc { get; set; }

        [JsonProperty("ETH")]
        public string Eth { get; set; }

        [JsonProperty("BCH")]
        public string Bch { get; set; }

        [JsonProperty("XRP")]
        public string Xrp { get; set; }

        [JsonProperty("LTC")]
        public string Ltc { get; set; }

        [JsonProperty("MIOTA")]
        public double Miota { get; set; }

        [JsonProperty("OMG")]
        public double Omg { get; set; }

        [JsonProperty("GNT")]
        public double Gnt { get; set; }
    }
}
