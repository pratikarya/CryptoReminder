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
using System;

namespace CryptoReminder.Droid.Views
{
    [Activity(Label = "")]
    public class CryptoCurrencyDetailView : BaseActivity<CryptoCurrencyDetailViewModel>
    {
        Toolbar _toolbar;
        LinearLayout _llInputs, _llCreateAlarm, _llUpdateAlarm, _llSaveAlarmChanges;
        TextView _toolBarTitle, _txtLowerLimit, _txtExactValue, _txtUpperLimit;
        EditText _etLowerLimit, _etExactValue, _etUpperLimit;
        Button _btnCreateAlarm, _btnEditAlarm, _btnRemoveAlarm, _btnSaveAlarmChanges;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.crypto_currency_detail_view);

            GetReferences();

            CreateUi();

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        protected override void OnResume()
        {
            base.OnResume();

            ViewModel.LoadCommand.Execute(null);
        }

        private void GetReferences()
        {
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _llUpdateAlarm = FindViewById<LinearLayout>(Resource.Id.llUpdateAlarm);
            _llInputs = FindViewById<LinearLayout>(Resource.Id.llInputs);
            _llCreateAlarm = FindViewById<LinearLayout>(Resource.Id.llCreateAlarm);
            _llSaveAlarmChanges = FindViewById<LinearLayout>(Resource.Id.llSaveAlarmChanges);
            _toolBarTitle = FindViewById<TextView>(Resource.Id.toolbarTitle);
            _txtLowerLimit = FindViewById<TextView>(Resource.Id.txtLowerLimit);
            _txtExactValue = FindViewById<TextView>(Resource.Id.txtExactValue);
            _txtUpperLimit = FindViewById<TextView>(Resource.Id.txtUpperLimit);
            _etLowerLimit = FindViewById<EditText>(Resource.Id.etLowerLimit);
            _etExactValue = FindViewById<EditText>(Resource.Id.etExactValue);
            _etUpperLimit = FindViewById<EditText>(Resource.Id.etUpperLimit);
            _btnCreateAlarm = FindViewById<Button>(Resource.Id.btnCreateAlarm);
            _btnEditAlarm = FindViewById<Button>(Resource.Id.btnEditAlarm);
            _btnRemoveAlarm = FindViewById<Button>(Resource.Id.btnRemoveAlarm);
            _btnSaveAlarmChanges = FindViewById<Button>(Resource.Id.btnSaveAlarmChanges);
        }

        private void CreateUi()
        {
            _btnEditAlarm.Click += _btnEditAlarm_Click;
            _btnSaveAlarmChanges.Click += _btnSaveAlarmChanges_Click;

            if (ViewModel.IsNew)
            {
                _toolBarTitle.Text = "New reminder for " + ViewModel.Reminder.MarketName;

                _llCreateAlarm.Visibility = ViewStates.Visible;
                _llUpdateAlarm.Visibility = ViewStates.Gone;
            }
            else
            {
                _llCreateAlarm.Visibility = ViewStates.Gone;
                _llUpdateAlarm.Visibility = ViewStates.Visible;

                _etLowerLimit.Enabled = false;
                _etExactValue.Enabled = false;
                _etUpperLimit.Enabled = false;
            }
        }

        private void _btnEditAlarm_Click(object sender, System.EventArgs e)
        {
            _llSaveAlarmChanges.Visibility = ViewStates.Visible;

            _etLowerLimit.Enabled = true;
            _etExactValue.Enabled = true;
            _etUpperLimit.Enabled = true;

            _etLowerLimit.RequestFocus();
        }

        private void _btnSaveAlarmChanges_Click(object sender, EventArgs e)
        {
            ViewModel.UpdateAlarmCommand.Execute(null);

            _llSaveAlarmChanges.Visibility = ViewStates.Gone;

            _etLowerLimit.Enabled = false;
            _etExactValue.Enabled = false;
            _etUpperLimit.Enabled = false;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Reminder")
            {
                _toolBarTitle.Text = ViewModel.Reminder.ToString();

                _txtExactValue.Text = ViewModel.Reminder.IsExactValueSet ? "Your current exact value : " + ViewModel.Reminder.ExactValue.ConvertExpo() : "Set exact value.";
                _txtLowerLimit.Text = ViewModel.Reminder.IsLowerLimitSet ? "Your current lower limit : " + ViewModel.Reminder.LowerLimit.ConvertExpo() : "Set lower limit.";
                _txtUpperLimit.Text = ViewModel.Reminder.IsUpperLimitSet ? "Your current upper limit : " + ViewModel.Reminder.UpperLimit.ConvertExpo() : "Set upper limit.";

            }
        }

        //private void _etSetAlarm_KeyPress(object sender, Android.Views.View.KeyEventArgs e)
        //{
        //    e.Handled = false;
        //    if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
        //    {
        //        ViewModel.SetAlarmCommand.Execute(null);
        //        e.Handled = true;
        //    }
        //}
    }
}