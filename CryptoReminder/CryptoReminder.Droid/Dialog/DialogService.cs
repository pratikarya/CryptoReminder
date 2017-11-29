﻿using System;
using CryptoReminder.Core.Dialog;
using CryptoReminder.Droid.Utilities;

namespace CryptoReminder.Droid
{
    public class DialogService : IDialogService
    {
        public void ShowDialog(bool isVisible, string message)
        {
            if(isVisible)
            {
                AndroidHUD.AndHUD.Shared.Show(Helper.CurrentActivity, message, -1, AndroidHUD.MaskType.Clear);
            }
            else
            {
                AndroidHUD.AndHUD.Shared.Dismiss(Helper.CurrentActivity);
            }
        }
    }
}