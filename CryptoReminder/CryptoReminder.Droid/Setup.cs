using Android.Content;
using CryptoReminder.Core.Dialog;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;

namespace CryptoReminder.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {

        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override void InitializeFirstChance()
        {
            Mvx.RegisterSingleton<IDialogService>(new DialogService());

            base.InitializeFirstChance();
        }
    }
}