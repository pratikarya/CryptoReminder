using CryptoReminder.Core.Common;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;
using CryptoReminder.Core.RealmService;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoReminder.Core.ViewModels
{
    public class CryptoCurrencyDetailViewModel : BaseViewModel
    {
        public ICryptoRealmService RealmService;

        public CryptoCurrencyDetailViewModel()
        {
            RealmService = new CryptoRealmService();
        }        

        public void Init(string cryptoCurrencyName)
        {
            CurrentMarketName = cryptoCurrencyName;
        }

        private bool _isAlarmSet;
        public bool IsAlarmSet
        {
            get { return _isAlarmSet; }
            set {
                _isAlarmSet = value;
                RaisePropertyChanged(() => IsAlarmSet);
            }
        }

        private string _currentMarketName;
        public string CurrentMarketName
        {
            get { return _currentMarketName; }
            set { _currentMarketName = value;
                RaisePropertyChanged(() => CurrentMarketName);
            }
        }

        private CryptoCurrencyReminderDto _cryptoCurrencyReminder;
        public CryptoCurrencyReminderDto CryptoCurrencyReminder
        {
            get { return _cryptoCurrencyReminder; }
            set {
                _cryptoCurrencyReminder = value;
                RaisePropertyChanged(() => CryptoCurrencyReminder);
            }
        }


        private CryptoCurrencyDto _currentCryptoCurrency;
        public CryptoCurrencyDto CurrentCryptoCurrency
        {
            get { return _currentCryptoCurrency; }
            set {
                _currentCryptoCurrency = value;
                RaisePropertyChanged(() => CurrentCryptoCurrency);
            }
        }

        private double _alarmValue;
        public double AlarmValue
        {
            get { return _alarmValue; }
            set {
                _alarmValue = value;
                RaisePropertyChanged(() => AlarmValue);
            }
        }

        private MvxCommand _setAlarmCommand;
        public ICommand SetAlarmCommand
        {
            get
            {
                _setAlarmCommand = _setAlarmCommand ?? new MvxCommand(DoSetAlarmCommand);
                return _setAlarmCommand;
            }
        }

        public void DoSetAlarmCommand()
        {
            Task.Run(() => SetAlarm());
        }

        public async void SetAlarm()
        {
            if (!IsAlarmSet)
            {
                if(string.IsNullOrEmpty(AlarmValue.ToString()) || AlarmValue == 0)
                {
                    return;
                }
            }

            await Task.Run(() =>
            {
                CryptoCurrencyReminder = new CryptoCurrencyReminderDto
                {
                    MarketName = CurrentMarketName,
                    IsAlarmSet = !IsAlarmSet,
                    Last = AlarmValue
                };

                RealmService.SaveReminder(CryptoCurrencyReminder);

                IsAlarmSet = !IsAlarmSet;

                if(IsAlarmSet)
                {
                    AlarmValue = 0;
                }
            });
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
            await Task.Run(() => {

                CryptoCurrencyReminder = RealmService.GetRemider(new CryptoCurrencyDto
                {
                    MarketName = CurrentMarketName
                });

                if (CryptoCurrencyReminder != null)
                    IsAlarmSet = CryptoCurrencyReminder.IsAlarmSet;
                else
                    IsAlarmSet = false;
            });
        }
    }
}
