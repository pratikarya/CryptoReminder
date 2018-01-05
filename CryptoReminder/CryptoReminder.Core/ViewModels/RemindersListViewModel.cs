using CryptoReminder.Core.Common;
using CryptoReminder.Core.CryptoCurrency;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;
using CryptoReminder.Core.Dialog;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoReminder.Core.ViewModels
{
    public class RemindersListViewModel : BaseViewModel
    {
        public IDialogService DialogService { get; set; }
        public ICryptoDelegate CryptoDelegate { get; set; }

        public RemindersListViewModel(IDialogService dialogService, ICryptoDelegate cryptoDelegate)
        {
            CryptoDelegate = cryptoDelegate;
            DialogService = dialogService;
        }

        public void Init(string cryptoCurrencyName)
        {
            Current = new CryptoCurrencyDto
            {
                MarketName = cryptoCurrencyName
            };
        }

        private CryptoCurrencyDto _current;
        public CryptoCurrencyDto Current
        {
            get { return _current; }
            set { _current = value; }
        }

        private string _remidersSearchString = "";
        public string RemindersSearchString
        {
            get { return _remidersSearchString; }
            set
            {
                _remidersSearchString = value;
                RaisePropertyChanged(() => RemindersSearchString);
                RaisePropertyChanged(() => SortedReminders);
            }
        }

        private List<CryptoCurrencyReminderDto> _reminders = new List<CryptoCurrencyReminderDto>();
        public List<CryptoCurrencyReminderDto> Reminders
        {
            get { return _reminders; }
            set
            {
                _reminders = value;
                RaisePropertyChanged(() => Reminders);
            }
        }

        private List<CryptoCurrencyReminderDto> _sortedReminders;
        public List<CryptoCurrencyReminderDto> SortedReminders
        {
            get { return Reminders.Where(x => x.MarketName.ToLower().Contains(RemindersSearchString.ToLower())).ToList(); }
            set
            {
                _sortedReminders = value;
                RaisePropertyChanged(() => SortedReminders);
            }
        }

        private MvxCommand _loadRemindersCommand;
        public ICommand LoadRemindersCommand
        {
            get
            {
                _loadRemindersCommand = _loadRemindersCommand ?? new MvxCommand(DoLoadRemindersCommand);
                return _loadRemindersCommand;
            }
        }

        public void DoLoadRemindersCommand()
        {
            Task.Run(() => DoLoadReminders());
        }

        public async void DoLoadReminders()
        {
            DialogService.ShowDialog(true);

            try
            {
                var searchDto = new ReminderSearchDto
                {
                    Type = SearchType.AllRemindersFor1Coin,
                    cryptoCurrency = Current
                };

                Reminders = await CryptoDelegate.GetReminder(searchDto);

                SortedReminders = Reminders;
            }
            catch (Exception ex)
            {

            }

            DialogService.ShowDialog(false);
        }

        #region Refresh Command

        private bool _isRemindersRefreshing;
        public bool IsRemindersRefreshing
        {
            get { return _isRemindersRefreshing; }
            set
            {
                _isRemindersRefreshing = value;
                RaisePropertyChanged(() => IsRemindersRefreshing);
            }
        }

        public ICommand ReloadReminders
        {
            get
            {
                return new MvxCommand(() =>
                {
                    IsRemindersRefreshing = true;
                    Task.Run(() => DoLoadReminders());
                    IsRemindersRefreshing = false;
                });
            }
        }

        #endregion

        private MvxCommand<CryptoCurrencyReminderDto> _selectReminderComand;
        public ICommand SelectReminderCommand
        {
            get
            {
                _selectReminderComand = _selectReminderComand ?? new MvxCommand<CryptoCurrencyReminderDto>(SelectReminder);
                return _selectReminderComand;
            }
        }        

        public void SelectReminder(CryptoCurrencyReminderDto reminder)
        {
            ShowViewModel<CryptoCurrencyDetailViewModel>(new { reminderId = reminder.Id, isNew = false});
        }

        private MvxCommand _newReminderCommand;
        public ICommand NewReminderCommand
        {
            get
            {
                _newReminderCommand = _newReminderCommand ?? new MvxCommand(NewReminder);
                return _newReminderCommand;
            }
        }

        public void NewReminder()
        {
            var reminder = new CryptoCurrencyReminderDto
            {
                MarketName = Current.MarketName
            };

            ShowViewModel<CryptoCurrencyDetailViewModel>(new { currentMarketName = Current.MarketName, isNew = true });
        }
    }
}
