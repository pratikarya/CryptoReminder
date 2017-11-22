using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using Newtonsoft.Json;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;

namespace CryptoReminder.Droid.Alarm
{
    [BroadcastReceiver]
    public class CryptoReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                var client = new WebClient();
                var data = client.DownloadString("https://bittrex.com/api/v1.1/public/getmarketsummaries");
                var currency = JsonConvert.DeserializeObject<CryptoCurrencyResponse>(data).Currencies;
                if (currency != null && currency.Count > 0)
                {
                    var message = "The last bid of " + currency[0].MarketName + " is " + currency[0].Last;

                    var expVal = currency[0].Last;
                    //var actualVal = expVal.TryParse()

                    //if(currency[0].Last > )
                    //{

                    Notification.Builder builder = new Notification.Builder(context)
                               .SetContentTitle("Crypto Reminder")
                               .SetSmallIcon(Resource.Drawable.notification_bg)
                               .SetContentText(message);

                    // Build the notification:
                    Notification notification = builder.Build();

                    // Get the notification manager:
                    NotificationManager notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;

                    // Publish the notification:
                    const int notificationId = 0;
                    notificationManager.Notify(notificationId, notification);

                    // }
                }
                
            }
            catch (Exception ex)
            {

            }
        }
    }
}