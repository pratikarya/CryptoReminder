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
using MvvmCross.Binding.Droid.Views;
using CryptoReminder.Core.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Support.V7.Widget;
using MvvmCross.Droid.Support.V7.RecyclerView;
using CryptoReminder.Droid.ViewHolders;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;

namespace CryptoReminder.Droid.Adapters
{
    public class CryptoReminderAdapter : MvxRecyclerAdapter
    {
        CryptoCurrencyListViewModel _vm;
        Activity _activity;

        public CryptoReminderAdapter(IMvxAndroidBindingContext bindingContext, CryptoCurrencyListViewModel vm, Activity activity)
          : base(bindingContext)
        {
            _vm = vm;
            _activity = activity;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, BindingContext.LayoutInflaterHolder);

            return new CryptoReminderViewHolder(InflateViewForHolder(parent, viewType, itemBindingContext), itemBindingContext, _vm, _activity)
            {
                Click = ItemClick,
                LongClick = ItemLongClick
            };
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);

            var aholder = holder as CryptoReminderViewHolder;

            var cryptoCurrency = (CryptoCurrencyReminderDto)GetItem(position);

            aholder.Configure(cryptoCurrency);
        }

        public override int GetItemViewType(int position)
        {
            return position;
        }
    }
}