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
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Platform;

namespace CryptoReminder.Droid.Utilities
{
    public static class Helper
    {
        public static Activity CurrentActivity
        {
            get
            {
                return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            }
        }
    }
}