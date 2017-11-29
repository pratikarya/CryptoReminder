using MvvmCross.Core.ViewModels;
using System;

namespace CryptoReminder.Core.Common
{
    public class BaseViewModel : MvxViewModel, IDisposable
    {


        public void Dispose()
        {

        }

        public virtual void DisposeViewModel()
        {

        }
    }
}
