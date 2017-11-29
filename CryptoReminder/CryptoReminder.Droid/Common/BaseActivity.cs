using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace CryptoReminder.Droid.Common
{
    public class BaseActivity<T> : MvxAppCompatActivity<T> where T : class, IMvxViewModel
    {
        public BaseActivity()
        {
            IsFirstLoad = true;
        }

        public bool IsFirstLoad { get; set; }
    }
}