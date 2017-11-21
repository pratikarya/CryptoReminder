using MvvmCross.Core.ViewModels;

namespace CryptoReminder.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            RegisterAppStart<HomeViewModel>();
        }
    }
}
