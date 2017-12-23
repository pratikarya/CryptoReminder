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
using Android.Appwidget;
using CryptoReminder.Core.CryptoCurrency;
using MvvmCross.Droid.Platform;
using System.Threading.Tasks;

namespace CryptoReminder.Droid.Widget.CryptoWidgetService
{
    [Service]
    public class CryptoUpdateService : Service
    {
        ICryptoDelegate CryptoDelegate;
        public async override void OnStart(Intent intent, int startId)
        {
            // Build the widget update for today
            RemoteViews updateViews = await BuildUpdate(this);

            // Push update for this widget to the home screen
            ComponentName thisWidget = new ComponentName(this, Java.Lang.Class.FromType(typeof(CryptoWidget)).Name);
            AppWidgetManager manager = AppWidgetManager.GetInstance(this);
            manager.UpdateAppWidget(thisWidget, updateViews);
        }

        public override IBinder OnBind(Intent intent)
        {
            // We don't need to bind to this service
            return null;
        }
        
        public async Task<RemoteViews> BuildUpdate(Context context)
        {
            var setupSingleton = MvxAndroidSetupSingleton.EnsureSingletonAvailable(context);
            setupSingleton.EnsureInitialized();
            
            CryptoDelegate = new CryptoDelegate();
            var cryptoCurrencies = await CryptoDelegate.GetCryptoCurrencyList();

            // Build an update that holds the updated widget contents
            var updateViews = new RemoteViews(context.PackageName, Resource.Layout.crypto_widget_view);

            //updateViews.SetRemoteAdapter()

            // When user clicks on widget, launch to Wiktionary definition page
            //if (!string.IsNullOrEmpty(entry.Link))
            //{
            //    Intent defineIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(entry.Link));

            //    PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, defineIntent, 0);
            //    updateViews.SetOnClickPendingIntent(Resource.Id.widget, pendingIntent);
            //}

            return updateViews;
        }
    }
}