using System;
using System.Globalization;

namespace CryptoReminder.Core.Utility
{
    public static class Helper
    {
        public static string ConvertExpo(this double value)
        {
            var strValue = Decimal.Parse(value.ToString(), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.Float, CultureInfo.InvariantCulture).ToString();

            return strValue;
        }
    }
}
