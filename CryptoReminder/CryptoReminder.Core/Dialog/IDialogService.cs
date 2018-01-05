using System;

namespace CryptoReminder.Core.Dialog
{
    public interface IDialogService
    {
        void ShowDialog(bool isVisible, string message = "Please wait!");

        void ShowSuccessDialog();

        void ShowErrorDialog();
    }
}
