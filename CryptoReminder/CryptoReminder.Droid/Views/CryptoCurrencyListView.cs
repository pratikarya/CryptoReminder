using Android.App;
using Android.OS;
using Android.Widget;
using CryptoReminder.Droid.Common;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Android.Support.V7.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using CryptoReminder.Droid.Adapters;
using CryptoReminder.Core.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Content;
using CryptoReminder.Droid.Alarm;
using System;
using Android.Graphics;

namespace CryptoReminder.Droid
{
    [Activity(MainLauncher = true)]
    public class CryptoCurrencyListView : BaseActivity<CryptoCurrencyListViewModel>
    {
        Toolbar _toolbar;
        TextView _toolBarTitle;
        LinearLayout _llCryptoCurrency, _llMyCryptoCurrency, _llTab;
        TextView _txtCryptoListTab, _txtMyCryptoListTab;
        MvxRecyclerView _rvCryptoCurrency, _rvMyCryptoCurrency;

        bool _isMyRemindersFirstLoad = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.crypto_currency_list_view);

            GetReferences();

            CreateUi();

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        protected override void OnResume()
        {
            base.OnResume();

            ViewModel.LoadCryptoCurrencyCommand.Execute(null);

            //start alarm manager.

            var alarmManager = (AlarmManager)GetSystemService(Context.AlarmService);
            var intent = new Intent(this, typeof(CryptoReceiver));
            var pendingIntent = PendingIntent.GetBroadcast(this, 0, intent, PendingIntentFlags.UpdateCurrent);
            alarmManager.SetRepeating(AlarmType.RtcWakeup, 1, 5000, pendingIntent);
        }

        protected override void OnPause()
        {
            base.OnPause();

        }

        private void GetReferences()
        {
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _toolBarTitle = FindViewById<TextView>(Resource.Id.toolbarTitle);
            _rvCryptoCurrency = FindViewById<MvxRecyclerView>(Resource.Id.rvCryptoCurrency);
            _rvMyCryptoCurrency = FindViewById<MvxRecyclerView>(Resource.Id.rvMyCryptoCurrency);
            _llCryptoCurrency = FindViewById<LinearLayout>(Resource.Id.llCryptoList);
            _llMyCryptoCurrency = FindViewById<LinearLayout>(Resource.Id.llMyCryptoList);
            _llTab = FindViewById<LinearLayout>(Resource.Id.llTab);
            _txtCryptoListTab = FindViewById<TextView>(Resource.Id.txtCryptoListTab);
            _txtMyCryptoListTab = FindViewById<TextView>(Resource.Id.txtMyCryptoListTab);
        }

        private void CreateUi()
        {
            _toolBarTitle.Text = "Crypto Reminder";
            SetupRecyclerView();
            SetBottomTabs();
        }

        private void SetupRecyclerView()
        {
            if (_rvCryptoCurrency != null)
            {
                _rvCryptoCurrency.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(this);
                layoutManager.Orientation = LinearLayoutManager.Vertical;
                _rvCryptoCurrency.SetLayoutManager(layoutManager);
                _rvCryptoCurrency.Adapter = new CryptoCurrencyAdapter((IMvxAndroidBindingContext)BindingContext, ViewModel, this);
                var mDividerItemDecoration = new DividerItemDecoration(_rvCryptoCurrency.Context, layoutManager.Orientation);
                _rvCryptoCurrency.AddItemDecoration(mDividerItemDecoration);
                _rvCryptoCurrency.SetItemAnimator(new DefaultItemAnimator());
            }

            if (_rvMyCryptoCurrency != null)
            {
                _rvMyCryptoCurrency.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(this);
                layoutManager.Orientation = LinearLayoutManager.Vertical;
                _rvMyCryptoCurrency.SetLayoutManager(layoutManager);
                _rvMyCryptoCurrency.Adapter = new CryptoReminderAdapter((IMvxAndroidBindingContext)BindingContext, ViewModel, this);
                var mDividerItemDecoration = new DividerItemDecoration(_rvMyCryptoCurrency.Context, layoutManager.Orientation);
                _rvMyCryptoCurrency.AddItemDecoration(mDividerItemDecoration);
                _rvMyCryptoCurrency.SetItemAnimator(new DefaultItemAnimator());
            }
        }

        private void SetBottomTabs()
        {
            _txtCryptoListTab.Click += _txtCryptoListTab_Click;
            _txtMyCryptoListTab.Click += _txtMyCryptoListTab_Click;

            _txtCryptoListTab.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
            _txtCryptoListTab.SetTextColor(Color.White);

            _txtMyCryptoListTab.SetTextSize(Android.Util.ComplexUnitType.Dip, 16);
            _txtMyCryptoListTab.SetTextColor(Color.LightGray);
        }

        private void _txtCryptoListTab_Click(object sender, EventArgs e)
        {
            _llCryptoCurrency.Visibility = Android.Views.ViewStates.Visible;
            _llMyCryptoCurrency.Visibility = Android.Views.ViewStates.Gone;

            _txtCryptoListTab.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
            _txtCryptoListTab.SetTextColor(Color.White);

            _txtMyCryptoListTab.SetTextSize(Android.Util.ComplexUnitType.Dip, 16);
            _txtMyCryptoListTab.SetTextColor(Color.LightGray);
        }

        private void _txtMyCryptoListTab_Click(object sender, EventArgs e)
        {
            _llCryptoCurrency.Visibility = Android.Views.ViewStates.Gone;
            _llMyCryptoCurrency.Visibility = Android.Views.ViewStates.Visible;

            _txtCryptoListTab.SetTextSize(Android.Util.ComplexUnitType.Dip, 16);
            _txtCryptoListTab.SetTextColor(Color.LightGray);

            _txtMyCryptoListTab.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
            _txtMyCryptoListTab.SetTextColor(Color.White);

            if (_isMyRemindersFirstLoad)
            {
                ViewModel.LoadMyCryptoCurrencyCommand.Execute(null);
                _isMyRemindersFirstLoad = false;
            }
        }
    }
}