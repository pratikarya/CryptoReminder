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
using MvvmCross.Droid.Support.V7.RecyclerView;
using CryptoReminder.Core.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;
using CryptoReminder.Core.Utility;

namespace CryptoReminder.Droid.ViewHolders
{
    public class CryptoReminderViewHolder : MvxRecyclerViewHolder
    {
        CryptoCurrencyListViewModel _vm;
        Activity _activity;
        TextView _txtMarketName, _txtLastBid;

        public CryptoReminderViewHolder(View itemView, IMvxAndroidBindingContext context, CryptoCurrencyListViewModel vm, Activity activity) : base(itemView, context)
        {
            _vm = vm;
            _activity = activity;
            _txtMarketName = itemView.FindViewById<TextView>(Resource.Id.txtMarketName);
            _txtLastBid = itemView.FindViewById<TextView>(Resource.Id.txtMarketLastBid);
        }

        public void Configure(CryptoCurrencyReminderDto cryptoCurrency)
        {
            //_txtLastBid.Text = "You have set your reminder at " + Helper.ConvertExpo(cryptoCurrency.LowerLimit) + " BTC.";
        }
    }
}