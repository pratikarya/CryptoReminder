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
using Android.Media;

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
                    var searchDto = new ReminderSearchDto
                    {
                        Type = SearchType.AllReminders
                    };

                    var reminders = RealmService.GetReminder(searchDto);
                    string message = "";

                    foreach(var reminder in reminders)
                    {
                        var cryptoCurrency = cryptoCurrencies.FirstOrDefault(x => x.MarketName == reminder.MarketName);

                        if (cryptoCurrency == null)
                            continue;

                        if(reminder.IsExactValueSet && cryptoCurrency.Last == reminder.ExactValue)
                        {
                            message += reminder.MarketName + " has reached " + reminder.ExactValue.ConvertExpo() + ". \n";
                        }

                        if (reminder.IsLowerLimitSet && cryptoCurrency.Last < reminder.LowerLimit)
                        {
                            message += reminder.MarketName + " has gone below " + reminder.LowerLimit.ConvertExpo() + ". It's current value is " + cryptoCurrency.Last.ConvertExpo() + ". \n";
                        }

                        if (reminder.IsUpperLimitSet && cryptoCurrency.Last > reminder.UpperLimit)
                        {
                            message += reminder.MarketName + " has gone above " + reminder.UpperLimit.ConvertExpo() + ". It's current value is " + cryptoCurrency.Last.ConvertExpo() + ". \n";
                        }
                    }

                    message.TrimEnd('\r', '\n');

                    if (string.IsNullOrEmpty(message))
                        return;

                    Notification.BigTextStyle textStyle = new Notification.BigTextStyle();
                    textStyle.BigText(message);
                    Notification.Builder builder = new Notification.Builder(context)
                                       .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
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