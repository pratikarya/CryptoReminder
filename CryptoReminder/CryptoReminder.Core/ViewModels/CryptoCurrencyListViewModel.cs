using CryptoReminder.Core.Common;
using CryptoReminder.Core.CryptoCurrency;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;
using CryptoReminder.Core.Dialog;
using CryptoReminder.Core.Koinex;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoReminder.Core.ViewModels
{
    public class CryptoCurrencyListViewModel : BaseViewModel
    {
        public IDialogService DialogService { get; set; }
        public ICryptoDelegate CryptoDelegate { get; set; }
        public IKoinexDelegate KoinexDelegate { get; set; }

        public CryptoCurrencyListViewModel(IDialogService dialogService, ICryptoDelegate cryptoDelegate, IKoinexDelegate koinexDelegate)
        {
            CryptoDelegate = cryptoDelegate;
            DialogService = dialogService;
            KoinexDelegate = koinexDelegate;
        }

        #region CrypotoCurrencyList

        private string _cryptoSearchString = "";
        public string CryptoSearchString
        {
            get { return _cryptoSearchString; }
            set
            {
                _cryptoSearchString = value;
                RaisePropertyChanged(() => CryptoSearchString);
                RaisePropertyChanged(() => SortedCryptoCurrencyList);
            }
        }

        private List<CryptoCurrencyDto> _cryptoCurrencyList = new List<CryptoCurrencyDto>();
        public List<CryptoCurrencyDto> CryptoCurrencyList
        {
            get { return _cryptoCurrencyList; }
            set
            {
                _cryptoCurrencyList = value;
                RaisePropertyChanged(() => CryptoCurrencyList);
            }
        }

        private List<CryptoCurrencyDto> _sortedListCyptoList;
        public List<CryptoCurrencyDto> SortedCryptoCurrencyList
        {
            get { return CryptoCurrencyList.Where(x => x.MarketName.ToLower().Contains(CryptoSearchString.ToLower())).ToList(); }
            set
            {
                _sortedListCyptoList = value;
                RaisePropertyChanged(() => SortedCryptoCurrencyList);
            }
        }

        private MvxCommand _loadCryptoCurrencyCommand;
        public ICommand LoadCryptoCurrencyCommand
        {
            get
            {
                _loadCryptoCurrencyCommand = _loadCryptoCurrencyCommand ?? new MvxCommand(DoLoadCryptoCurrencyCommand);
                return _loadCryptoCurrencyCommand;
            }
        }

        public void DoLoadCryptoCurrencyCommand()
        {
            Task.Run(() => DoLoadCryptoCurrency());
        }

        public async void DoLoadCryptoCurrency()
        {
            DialogService.ShowDialog(true);

            try
            {
                var koinex = await KoinexDelegate.GetKoinexList();

                CryptoCurrencyList = await CryptoDelegate.GetCryptoCurrencyList();

                SortedCryptoCurrencyList = CryptoCurrencyList;
            }
            catch (Exception ex)
            {

            }

            DialogService.ShowDialog(false);
        }

        #region Refresh Command

        private bool _isCryptoCurrencyRefreshing;
        public bool IsCryptoCurrencyRefreshing
        {
            get { return _isCryptoCurrencyRefreshing; }
            set
            {
                _isCryptoCurrencyRefreshing = value;
                RaisePropertyChanged(() => IsCryptoCurrencyRefreshing);
            }
        }

        public ICommand ReloadCryptoCurrencyCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    IsCryptoCurrencyRefreshing = true;
                    Task.Run(() => DoLoadCryptoCurrency());
                    IsCryptoCurrencyRefreshing = false;
                });
            }
        }

        #endregion

        private MvxCommand<CryptoCurrencyDto> _selectedCryptoItemCommand;
        public ICommand SelectedCryptoItemCommand
        {
            get
            {
                _selectedCryptoItemCommand = _selectedCryptoItemCommand ?? new MvxCommand<CryptoCurrencyDto>(DoSelectedCryptoItemCommand);
                return _selectedCryptoItemCommand;
            }
        }

        public void DoSelectedCryptoItemCommand(CryptoCurrencyDto cryptoCurrency)
        {
            ShowViewModel<CryptoCurrencyDetailViewModel>(new { cryptoCurrencyName = cryptoCurrency.MarketName });
        }

        #endregion

        #region MyCryptoCurrencyList
        
        private string _myCryptoSearchString = "";
        public string MyCryptoSearchString
        {
            get { return _myCryptoSearchString; }
            set
            {
                _myCryptoSearchString = value;
                RaisePropertyChanged(() => MyCryptoSearchString);
                RaisePropertyChanged(() => SortedMyCryptoCurrencyList);
            }
        }

        private List<CryptoCurrencyReminderDto> _myCryptoCurrencyList = new List<CryptoCurrencyReminderDto>();
        public List<CryptoCurrencyReminderDto> MyCryptoCurrencyList
        {
            get { return _myCryptoCurrencyList; }
            set
            {
                _myCryptoCurrencyList = value;
                RaisePropertyChanged(() => MyCryptoCurrencyList);
            }
        }

        private List<CryptoCurrencyReminderDto> _sortedMyCryptoCurrencyList;
        public List<CryptoCurrencyReminderDto> SortedMyCryptoCurrencyList
        {
            get { return MyCryptoCurrencyList.Where(x => x.MarketName.ToLower().Contains(MyCryptoSearchString.ToLower())).ToList(); }
            set
            {
                _sortedMyCryptoCurrencyList = value;
                RaisePropertyChanged(() => SortedMyCryptoCurrencyList);
            }
        }
        
        private MvxCommand _loadMyCryptoCurrencyCommand;
        public ICommand LoadMyCryptoCurrencyCommand
        {
            get
            {
                _loadMyCryptoCurrencyCommand = _loadMyCryptoCurrencyCommand ?? new MvxCommand(DoLoadMyCryptoCurrencyCommand);
                return _loadMyCryptoCurrencyCommand;
            }
        }

        public void DoLoadMyCryptoCurrencyCommand()
        {
            Task.Run(() => DoLoadMyCryptoCurrency());
        }

        public async void DoLoadMyCryptoCurrency()
        {
            DialogService.ShowDialog(true);

            try
            {
                MyCryptoCurrencyList = await CryptoDelegate.GetMyCryptoCurrencyList();

                SortedMyCryptoCurrencyList = MyCryptoCurrencyList;
            }
            catch (Exception ex)
            {

            }

            DialogService.ShowDialog(false);
        }

        #region Refresh Command

        private bool _isMyCryptoCurrencyRefreshing;
        public bool IsMyCryptoCurrencyRefreshing
        {
            get { return _isMyCryptoCurrencyRefreshing; }
            set
            {
                _isMyCryptoCurrencyRefreshing = value;
                RaisePropertyChanged(() => IsMyCryptoCurrencyRefreshing);
            }
        }

        public ICommand ReloadMyCryptoCurrencyCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    IsMyCryptoCurrencyRefreshing = true;
                    Task.Run(() => DoLoadMyCryptoCurrency());
                    IsMyCryptoCurrencyRefreshing = false;
                });
            }
        }

        #endregion

        private MvxCommand<CryptoCurrencyReminderDto> _selectedMyCryptoItemCommand;
        public ICommand SelectedMyCryptoItemCommand
        {
            get
            {
                _selectedMyCryptoItemCommand = _selectedMyCryptoItemCommand ?? new MvxCommand<CryptoCurrencyReminderDto>(DoSelectedMyCryptoItemCommand);
                return _selectedMyCryptoItemCommand;
            }
        }

        public void DoSelectedMyCryptoItemCommand(CryptoCurrencyReminderDto cryptoCurrency)
        {
            ShowViewModel<CryptoCurrencyDetailViewModel>(new { cryptoCurrencyName = cryptoCurrency.MarketName });
        }

        #endregion

        public override void DisposeViewModel()
        {
            base.DisposeViewModel();

        }
    }
}
