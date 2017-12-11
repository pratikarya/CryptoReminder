using System;
using Android.App;
using Android.Content;
using System.Net;
using Newtonsoft.Json;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;
using CryptoReminder.Core.RealmService;
using System.Linq;
using CryptoReminder.Core.CryptoCurrency;
using CryptoReminder.Core.Utility;
using MvvmCross.Droid.Platform;

namespace CryptoReminder.Droid.Alarm
{
    [BroadcastReceiver]
    public class CryptoReceiver : BroadcastReceiver
    {
        ICryptoDelegate CryptoDelegate;
        ICryptoRealmService RealmService;

        public CryptoReceiver()
        {
        }

        public async override void OnReceive(Context context, Intent intent)
        {
            try
            {
                var setupSingleton = MvxAndroidSetupSingleton.EnsureSingletonAvailable(context);
                setupSingleton.EnsureInitialized();

                RealmService = new CryptoRealmService();
                CryptoDelegate = new CryptoDelegate();

                var cryptoCurrencies = await CryptoDelegate.GetCryptoCurrencyList();
                if (cryptoCurrencies != null && cryptoCurrencies.Count > 0)
                {
                    var reminders = RealmService.GetReminders();
                    string message = "";

                    foreach(var reminder in reminders)
                    {
                        if(reminder.IsAlarmSet)
                        {
                            var cryptoCurrency = cryptoCurrencies.FirstOrDefault(x => x.MarketName == reminder.MarketName && x.Last > reminder.Last);

                            if (cryptoCurrency != null)
                            {
                                message += "The last bid of " + reminder.MarketName + " is " + Helper.ConvertExpo(cryptoCurrency.Last) + " BTC.\n";
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(message))
                        return;

                    Notification.BigTextStyle textStyle = new Notification.BigTextStyle();
                    textStyle.BigText(message);
                    Notification.Builder builder = new Notification.Builder(context)
                                       .SetContentTitle("Crypto Reminder.")
                                       .SetSmallIcon(Resource.Drawable.notification_bg)
                                       .SetStyle(textStyle);

                    // Build the notification:
                    Notification notification = builder.Build();

                    // Get the notification manager:
                    NotificationManager notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;

                    // Publish the notification:
                    const int notificationId = 0;
                    notificationManager.Notify(notificationId, notification);
                }
                
            }
            catch (Exception ex)
            {

            }
        }
    }
}