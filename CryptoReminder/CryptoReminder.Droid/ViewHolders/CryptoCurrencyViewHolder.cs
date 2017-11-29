using Android.App;
using Android.Views;
using Android.Widget;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;
using CryptoReminder.Core.Utility;
using CryptoReminder.Core.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace CryptoReminder.Droid.ViewHolders
{
    public class CryptoCurrencyViewHolder : MvxRecyclerViewHolder
    {
        CryptoCurrencyListViewModel _vm;
        Activity _activity;
        TextView _txtMarketName, _txtLastBid;

        public CryptoCurrencyViewHolder(View itemView, IMvxAndroidBindingContext context, CryptoCurrencyListViewModel vm, Activity activity) : base(itemView, context)
        {
            _vm = vm;
            _activity = activity;
            _txtMarketName = itemView.FindViewById<TextView>(Resource.Id.txtMarketName);
            _txtLastBid = itemView.FindViewById<TextView>(Resource.Id.txtMarketLastBid);
        }

        public void Configure(CryptoCurrencyDto cryptoCurrency)
        {
            _txtLastBid.Text = "Last Bid : " + Helper.ConvertExpo(cryptoCurrency.Last) + " BTC.";
        }
    }
}