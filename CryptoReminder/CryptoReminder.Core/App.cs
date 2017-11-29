using CryptoReminder.Core.CryptoCurrency;
using CryptoReminder.Core.RealmService;
using CryptoReminder.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace CryptoReminder.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            Mvx.RegisterSingleton<ICryptoRealmService>(new CryptoRealmService());
            Mvx.RegisterSingleton<ICryptoDelegate>(new CryptoDelegate());

            RegisterNavigationServiceAppStart<CryptoCurrencyListViewModel>();
        }
    }
}
