using CryptoTradingBot.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebActions;
using WebActions.Browsers.Chrome;

namespace CryptoTradingBot.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public RelayCommand StartTradingCommand { get; set; }
        public RelayCommand StopTradingCommand { get; set; }
        public static ObservableCollection<string> CoinList { get; set; }

        private string selectedCoin;
        public string SelectedCoin
        {
            get { return this.selectedCoin; }
            set
            {
                this.SetProperty(ref this.selectedCoin, value);
            }
        }
        protected virtual void SetProperty<T>(
                       ref T member,
                       T val,
                       [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;
            member = val;
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("SelectedCoin"))
            {

            }

        }

        public MainWindowViewModel()
        {
            this.StartTradingCommand = new RelayCommand(this.OnStartTrading);
            this.StopTradingCommand = new RelayCommand(this.OnStopTrading);
            this.PropertyChanged += new PropertyChangedEventHandler(this.OnPropertyChanged);
            CoinList = new ObservableCollection<string> { "https://www.binance.com/en/trade/ADA_BNB?type=spot", "https://www.binance.com/en/trade/LTC_BNB?type=spot" };
            SelectedCoin = CoinList.First();

        }

        private void OnStartTrading()
        {
            MessageBox.Show("Started Trading");
            Trading.StartTrading(SelectedCoin);
        }

        private void OnStopTrading()
        {
            MessageBox.Show("Stopped Trading");
            Trading.StopTrading();
        }
    }
}
