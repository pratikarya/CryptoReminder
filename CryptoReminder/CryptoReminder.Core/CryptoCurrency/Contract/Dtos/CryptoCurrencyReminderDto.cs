﻿namespace CryptoReminder.Core.CryptoCurrency.Contract.Dtos
{
    public class CryptoCurrencyReminderDto
    {
        public int Id { get; set; }
        public string MarketName { get; set; }
        public double LowerLimit { get; set; }
        public double ExactValue { get; set; }
        public double UpperLimit { get; set; }
        public bool IsLowerLimitSet
        {
            get
            {
                return LowerLimit > 0;
            }
        }
        public bool IsExactValueSet
        {
            get
            {
                return ExactValue > 0;
            }
        }
        public bool IsUpperLimitSet
        {
            get
            {
                return UpperLimit > 0;
            }
        }
        public override string ToString()
        {
            return MarketName + " Reminder " + Id;
        }
    }
}