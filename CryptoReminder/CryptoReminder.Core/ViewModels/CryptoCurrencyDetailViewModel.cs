using CryptoReminder.Core.Common;
using CryptoReminder.Core.CryptoCurrency;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;
using CryptoReminder.Core.Dialog;
using CryptoReminder.Core.RealmService;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoReminder.Core.ViewModels
{
    public class CryptoCurrencyDetailViewModel : BaseViewModel
    {
        //public ICryptoRealmService RealmService;
        public IDialogService DialogService;
        public ICryptoDelegate CryptoDelegate { get; set; }

        public CryptoCurrencyDetailViewModel(IDialogService dialogService, ICryptoDelegate cryptoDelegate)
        {
            CryptoDelegate = cryptoDelegate;
            DialogService = dialogService;
        }             

        public void Init(int reminderId, string currentMarketName, bool isNew)
        {
            IsNew = isNew;
            if(IsNew)
            {
                Reminder = new CryptoCurrencyReminderDto
                {
                    MarketName = currentMarketName
                };
            }
            else
            {
                Reminder = new CryptoCurrencyReminderDto
                {
                    Id = reminderId
                };
            }
        }

        private bool _isNew;
        public bool IsNew
        {
            get { return _isNew; }
            set {
                _isNew = value;
                RaisePropertyChanged(() => IsNew);
            }
        }

        //private string _currentMarketName;
        //public string CurrentMarketName
        //{
        //    get { return _currentMarketName; }
        //    set
        //    {
        //        _currentMarketName = value;
        //        RaisePropertyChanged(() => CurrentMarketName);
        //    }
        //}

        //private int _reminderId;
        //public int ReminderId
        //{
        //    get { return _reminderId; }
        //    set { _reminderId = value;
        //        RaisePropertyChanged(() => ReminderId);
        //    }
        //}


        private CryptoCurrencyReminderDto _reminder;
        public CryptoCurrencyReminderDto Reminder
        {
            get { return _reminder; }
            set {
                _reminder = value;
                RaisePropertyChanged(() => Reminder);
            }
        }
        
        //private double _lowerLimit;
        //public double LowerLimit
        //{
        //    get { return _lowerLimit; }
        //    set {
        //        _lowerLimit = value;
        //        RaisePropertyChanged(() => LowerLimit);
        //    }
        //}

        //private double _exactValue;
        //public double ExactValue
        //{
        //    get { return _exactValue; }
        //    set {
        //        _exactValue = value;
        //        RaisePropertyChanged(() => ExactValue);
        //    }
        //}

        //private double _upperLimit;
        //public double UpperLimit
        //{
        //    get { return _upperLimit; }
        //    set {
        //        _upperLimit = value;
        //        RaisePropertyChanged(() => UpperLimit);
        //    }
        //}
        
        private MvxCommand _updateAlarmCommand;
        public ICommand UpdateAlarmCommand
        {
            get
            {
                _updateAlarmCommand = _updateAlarmCommand ?? new MvxCommand(DoUpdateAlarmCommand);
                return _updateAlarmCommand;
            }
        }

        public void DoUpdateAlarmCommand()
        {
            Task.Run(() => UpdateAlarm());
        }

        public async void UpdateAlarm()
        {
            if (!Reminder.IsExactValueSet && !Reminder.IsLowerLimitSet && !Reminder.IsUpperLimitSet)
            {
                //no value set 
                DialogService.ShowErrorDialog();
                //await Task.Delay(2000);
                return;
            }

            Reminder = await CryptoDelegate.SaveReminder(Reminder);

            DialogService.ShowSuccessDialog();
            //await Task.Delay(2000);
            Close(this);
        }

        private MvxCommand _removeAlarmCommand;
        public ICommand RemoveAlarmCommand
        {
            get
            {
                _removeAlarmCommand = _removeAlarmCommand ?? new MvxCommand(DoRemoveAlarmCommand);
                return _removeAlarmCommand;
            }
        }

        public void DoRemoveAlarmCommand()
        {
            Task.Run(() => RemoveAlarm());
        }

        public async void RemoveAlarm()
        {
            var success = await CryptoDelegate.RemoveReminder(Reminder);
            if(success)
            {
                DialogService.ShowSuccessDialog();
                //await Task.Delay(2000);
                Close(this);
            }
        }

        private MvxCommand _loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                _loadCommand = _loadCommand ?? new MvxCommand(DoLoadCommand);
                return _loadCommand;
            }
        }

        public void DoLoadCommand()
        {
            Task.Run(() => DoLoad());
        }

        public async void DoLoad()
        {
            if (IsNew)
                return;

            Reminder = await CryptoDelegate.GetReminderDetails(Reminder);
        }
    }
}
