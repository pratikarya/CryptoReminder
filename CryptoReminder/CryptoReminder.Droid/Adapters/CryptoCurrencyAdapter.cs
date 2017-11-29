using Android.App;
using Android.Views;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Support.V7.Widget;
using CryptoReminder.Droid.ViewHolders;
using CryptoReminder.Core.ViewModels;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;

namespace CryptoReminder.Droid.Adapters
{
    public class CryptoCurrencyAdapter : MvxRecyclerAdapter
    {
        CryptoCurrencyListViewModel _vm;
        Activity _activity;

        public CryptoCurrencyAdapter(IMvxAndroidBindingContext bindingContext, CryptoCurrencyListViewModel vm, Activity activity)
          : base(bindingContext)
        {
            _vm = vm;
            _activity = activity;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, BindingContext.LayoutInflaterHolder);

            return new CryptoCurrencyViewHolder(InflateViewForHolder(parent, viewType, itemBindingContext), itemBindingContext, _vm, _activity)
            {
                Click = ItemClick,
                LongClick = ItemLongClick
            };
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);

            var aholder = holder as CryptoCurrencyViewHolder;

            var cryptoCurrency = (CryptoCurrencyDto)GetItem(position);

            aholder.Configure(cryptoCurrency);
        }

        public override int GetItemViewType(int position)
        {
            return position;
        }
    }
}