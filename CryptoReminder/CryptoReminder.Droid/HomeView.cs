using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;
using CryptoReminder.Core;
using System;
using System.Net;
using Newtonsoft.Json;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;
using System.Collections.Generic;
using CryptoReminder.Droid.Service;
using Android.Content;
using Android.Support.V4.App;
using Android.Widget;
using CryptoReminder.Droid.Alarm;

namespace CryptoReminder.Droid
{
    [Activity(Label = "", MainLauncher = true)]
    public class HomeView : MvxActivity<HomeViewModel>
    {
        Button _btnShow;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.home_view);
        }

        protected override void OnResume()
        {
            base.OnResume();

            var alarmManager = (AlarmManager)GetSystemService(Context.AlarmService);

            var intent = new Intent(this, typeof(CryptoReceiver));
            var pending = PendingIntent.GetBroadcast(this, 0, intent, PendingIntentFlags.UpdateCurrent);
            alarmManager.SetRepeating(AlarmType.RtcWakeup, 1, 60 * 1000, pending);

            //StartService(new Intent(this, typeof(CryptoReminderService)));
        }

        protected override void OnPause()
        {
            base.OnPause();

            //StopService(new Intent(this, typeof(CryptoReminderService)));
        }
    }
}