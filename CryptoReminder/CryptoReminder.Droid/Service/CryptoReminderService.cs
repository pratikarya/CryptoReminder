using System;
using Android.App;
using Android.Content;
using Android.OS;
using System.Threading;
using Android.Runtime;
using Android.Util;
using System.Net;
using Newtonsoft.Json;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;

namespace CryptoReminder.Droid.Service
{
    [Service]
    public class CryptoReminderService : Android.App.Service
    {
        static readonly string TAG = "X:" + typeof(CryptoReminderService).Name;
        static readonly int TimerWait = 4000;
        Timer timer;
        DateTime startTime;
        bool isStarted = false;
        bool dataLoading = false;

        public override void OnCreate()
        {
            base.OnCreate();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Log.Debug(TAG, $"OnStartCommand called at {startTime}, flags={flags}, startid={startId}");
            if (isStarted)
            {
                TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
                Log.Debug(TAG, $"This service was already started, it's been running for {runtime:c}.");
            }
            else
            {
                startTime = DateTime.UtcNow;
                Log.Debug(TAG, $"Starting the service, at {startTime}.");
                timer = new Timer(HandleTimerCallback, startTime, 0, TimerWait);
                isStarted = true;
            }

            

            return StartCommandResult.NotSticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }


        public override void OnDestroy()
        {
            timer.Dispose();
            timer = null;
            isStarted = false;

            TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
            Log.Debug(TAG, $"Simple Service destroyed at {DateTime.UtcNow} after running for {runtime:c}.");
            base.OnDestroy();
        }

        void HandleTimerCallback(object state)
        {
            //TimeSpan runTime = DateTime.UtcNow.Subtract(startTime);
            //Log.Debug(TAG, $"This service has been running for {runTime:c} (since ${state}).");

            if(dataLoading)
            {
                return;
            }

            try
            {
                dataLoading = true;

                var client = new WebClient();
                var data = client.DownloadString("https://bittrex.com/api/v1.1/public/getmarketsummaries");
                var currency = JsonConvert.DeserializeObject<CryptoCurrencyResponse>(data).Currencies;
                if(currency != null && currency.Count > 0)
                {
                    var message = "The last bid of " + currency[0].MarketName + " is " + currency[0].Last;

                    var expVal = currency[0].Last;
                    //var actualVal = expVal.TryParse()

                    //if(currency[0].Last > )
                    //{

                        Notification.Builder builder = new Notification.Builder(this)
                                   .SetContentTitle("Crypto Reminder")
                                   .SetSmallIcon(Resource.Drawable.notification_bg)
                                   .SetContentText(message);

                        // Build the notification:
                        Notification notification = builder.Build();

                        // Get the notification manager:
                        NotificationManager notificationManager =
                            GetSystemService(Context.NotificationService) as NotificationManager;

                        // Publish the notification:
                        const int notificationId = 0;
                        notificationManager.Notify(notificationId, notification);

                   // }
                }

                dataLoading = false;
            }
            catch (Exception ex)
            {

            }

        }
    }
}