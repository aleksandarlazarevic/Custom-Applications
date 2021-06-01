﻿using CryptoTradingBot.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
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
        private string apiKey;
        public string ApiKey
        {
            get { return this.apiKey; }
            set
            {
                this.SetProperty(ref this.apiKey, value);
            }
        }
        private string apiSecret;
        public string ApiSecret
        {
            get { return this.apiSecret; }
            set
            {
                this.SetProperty(ref this.apiSecret, value);
            }
        }
        private string panicSellPercentage;
        public string PanicSellPercentage
        {
            get { return this.panicSellPercentage; }
            set
            {
                this.SetProperty(ref this.panicSellPercentage, value);
            }
        }
        private string panicSellTimeout;

        public SolidColorBrush BackgroundColor { get; set; }

        public string PanicSellTimeout
        {
            get { return this.panicSellTimeout; }
            set
            {
                this.SetProperty(ref this.panicSellTimeout, value);
            }
        }
        private string averageValueCalculationPeriod;
        public string AverageValueCalculationPeriod
        {
            get { return this.averageValueCalculationPeriod; }
            set
            {
                this.SetProperty(ref this.averageValueCalculationPeriod, value);
            }
        }

        private string sellingPercentage;
        public string SellingPercentage
        {
            get { return this.sellingPercentage; }
            set
            {
                this.SetProperty(ref this.sellingPercentage, value);
            }
        }
        
        private string buyingMargin;
        public string BuyingThreshold
        {
            get { return this.buyingMargin; }
            set
            {
                this.SetProperty(ref this.buyingMargin, value);
            }
        }
        
        private bool calculateBuyingThresholdAutomatically;
        public bool CalculateBuyingThresholdAutomatically
        {
            get { return this.calculateBuyingThresholdAutomatically; }
            set
            {
                this.SetProperty(ref this.calculateBuyingThresholdAutomatically, value);
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
            if (e.PropertyName.Equals("CalculateBuyingThresholdAutomatically"))
            {
                Trading.CalculateBuyingThresholdAutomatically = CalculateBuyingThresholdAutomatically;
            }
        }

        public MainWindowViewModel()
        {
            BackgroundColor = Brushes.SkyBlue;
            PanicSellPercentage = "90";
            PanicSellTimeout = "60";
            AverageValueCalculationPeriod = "20";
            SellingPercentage = "0.35";
            BuyingThreshold = "0";
            CalculateBuyingThresholdAutomatically = false;
            //BinanceApi.TestApi();
            this.StartTradingCommand = new RelayCommand(this.OnStartTrading);
            this.StopTradingCommand = new RelayCommand(this.OnStopTrading);
            this.PropertyChanged += new PropertyChangedEventHandler(this.OnPropertyChanged);
            CoinList = new ObservableCollection<string> { "BNB", "LTC", "ADA", "TUSD" };
            SelectedCoin = CoinList.First();
            Coin.Name = SelectedCoin;
            Coin.Pair = Coin.Name + "USDT";
        }

        private void OnStartTrading()
        {
            Trading.apiKey = ApiKey;
            Trading.secretKey = ApiSecret;
            Trading.PanicSellPercentage = decimal.Parse(this.PanicSellPercentage, CultureInfo.InvariantCulture)/100;
            Trading.PanicSellTimeout = decimal.Parse(this.PanicSellTimeout, CultureInfo.InvariantCulture);
            Trading.BuyingThreshold = decimal.Parse(this.BuyingThreshold, CultureInfo.InvariantCulture);
            Trading.CalculateBuyingThresholdAutomatically = CalculateBuyingThresholdAutomatically;

            Trading.SellingPercentage = 1 + decimal.Parse(this.SellingPercentage, CultureInfo.InvariantCulture)/100;
            Trading.AverageValueCalculationPeriod = int.Parse(this.AverageValueCalculationPeriod, CultureInfo.InvariantCulture);
            Values.GetAccountInfo();
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
