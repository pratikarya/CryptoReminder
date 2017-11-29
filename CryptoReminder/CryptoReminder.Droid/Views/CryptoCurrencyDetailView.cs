using Android.App;
using Android.OS;
using Android.Widget;
using CryptoReminder.Droid.Common;
using CryptoReminder.Core.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Graphics;
using Android.Content;
using CryptoReminder.Droid.Alarm;
using CryptoReminder.Core.Utility;
using Android.Views;

namespace CryptoReminder.Droid.Views
{
    [Activity(Label = "")]
    public class CryptoCurrencyDetailView : BaseActivity<CryptoCurrencyDetailViewModel>
    {
        Toolbar _toolbar;
        TextView _toolBarTitle, _txtAlarm;
        EditText _etSetAlarm;
        Button _btnSetAlarm;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.crypto_currency_detail_view);

            GetReferences();

            CreateUi();

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "CryptoCurrencyReminder")
            {
                if (ViewModel.CryptoCurrencyReminder != null && ViewModel.CryptoCurrencyReminder.IsAlarmSet)
                {
                    _txtAlarm.Visibility = Android.Views.ViewStates.Visible;
                    _etSetAlarm.Visibility = Android.Views.ViewStates.Gone;
                    _txtAlarm.Text = "Alarm has been set for " + ViewModel.CurrentMarketName + " at " + Helper.ConvertExpo(ViewModel.CryptoCurrencyReminder.Last) + " BTC.";
                    _btnSetAlarm.SetText(Resource.String.btnRemovealarmTitle);
                }
                else
                {
                    _txtAlarm.Visibility = Android.Views.ViewStates.Gone;
                    _etSetAlarm.Visibility = Android.Views.ViewStates.Visible;
                    _btnSetAlarm.SetText(Resource.String.btnSetalarmTitle);
                }
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            ViewModel.LoadCommand.Execute(null);
            
            var reminders = ViewModel.RealmService.GetReminders();

            if(reminders == null || reminders.Count == 0)
            {
                //start alarm manager.

                var alarmManager = (AlarmManager) GetSystemService(Context.AlarmService);
                var intent = new Intent(this, typeof(CryptoReceiver));
                var pendingIntent = PendingIntent.GetBroadcast(this, 0, intent, PendingIntentFlags.UpdateCurrent);
                alarmManager.SetRepeating(AlarmType.RtcWakeup, 1, 60000, pendingIntent);
            }

            _etSetAlarm.SetHint(Resource.String.etHintReminder);
        }

        private void GetReferences()
        {
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _toolBarTitle = FindViewById<TextView>(Resource.Id.toolbarTitle);
            _txtAlarm = FindViewById<TextView>(Resource.Id.txtAlarm);
            _btnSetAlarm = FindViewById<Button>(Resource.Id.btnSetAlarm);
            _etSetAlarm = FindViewById<EditText>(Resource.Id.etSetAlarmValue);
        }

        private void CreateUi()
        {
            _toolBarTitle.Text = ViewModel.CurrentMarketName;
            _btnSetAlarm.SetTextColor(Color.Black);
            _etSetAlarm.KeyPress += _etSetAlarm_KeyPress;
        }

        private void _etSetAlarm_KeyPress(object sender, Android.Views.View.KeyEventArgs e)
        {
            e.Handled = false;
            if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            {
                ViewModel.SetAlarmCommand.Execute(null);
                e.Handled = true;
            }
        }
    }
}