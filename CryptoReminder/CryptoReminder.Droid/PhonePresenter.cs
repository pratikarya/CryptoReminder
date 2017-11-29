using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Views;

namespace CryptoReminder.Droid
{
    internal class PhonePresenter : IMvxAndroidViewPresenter
    {
        public void AddPresentationHintHandler<THint>(Func<THint, bool> action) where THint : MvxPresentationHint
        {

        }

        public void ChangePresentation(MvxPresentationHint hint)
        {

        }

        public void Close(IMvxViewModel toClose)
        {

        }

        public void Show(MvxViewModelRequest request)
        {

        }
    }
}