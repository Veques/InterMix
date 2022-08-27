using Intermix.Commands;
using Intermix.Enums;
using Intermix.Models.MainWindowModels;
using Intermix.Stores;
using Intermix.ViewModels.Base;
using Intermix.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Intermix.ViewModels.CurrencyConverter
{
    public class CurrencyConverterViewModel : BaseViewModel
    {
        #region CONST
        private const string BASE_HTTPS_URL = @"https://api.currencyapi.com/v3/latest?";
        private const string CURRENCY_FROM = "currencies=";
        private const string CURRENCY_TO = "&base_currency=";
        private const string API_KEY = "apikey=fc3685a0-39a4-11ec-87f9-a764dd5862af&";
        private const string URL = @"https://currencyapi.com/docs/currency-list#currency-list";
        #endregion

        public ICommand BackMainCommand { get; set; }
        public ICommand ConvertCommand { get; set; }

        #region Constructor

        public CurrencyConverterViewModel(NavigationStore navigationStore)
        {
            BackMainCommand = new NavigationCommand<ChooseActivityViewModel>(navigationStore,
                () => new ChooseActivityViewModel(navigationStore),
                x => true);

            ConvertCommand = new RelayCommand(o => ConvertCurrencies(), o => true);
            LoadMethodAsync();
        }
        #endregion

        private async Task LoadMethodAsync()
        {
            List<List<string>> tableData = new(await GetCurrencies());
            List<string> tableValues = new();


            tableValues = tableData.SelectMany(x => x).ToList();

            Currencies = new ObservableCollection<Currency>();

            for (int i = 0; i < tableValues.Count; i += 2)
            {
                string x = tableValues[i];
                string y = tableValues[i + 1];

                Currencies.Add(new Currency { ShortName = x, LongName = y });
            }
        }

        private static async Task<List<List<string>>> GetCurrencies()
        {

            using var client = new HttpClient();

            var html = await client.GetStringAsync(URL);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            var table = doc.DocumentNode.SelectSingleNode("//table");
            return table.Descendants("tr")
                        .Skip(2)
                        .Select(tr => tr.Descendants("td")
                                        .Select(td => WebUtility.HtmlDecode(td.InnerText))
                                        .ToList())
                        .ToList();
        }

        private async void ConvertCurrencies()
        {
            try
            {
                using var client = new HttpClient();
                string primary = Currencies.Select(x => x.ShortName).ElementAt(PrimaryCurrency);
                string secondary = Currencies.Select(x => x.ShortName).ElementAt(SecondaryCurrency);

                var response = await client.GetAsync(BASE_HTTPS_URL + API_KEY + CURRENCY_FROM + secondary + CURRENCY_TO + primary);
                var stringResult = await response.Content.ReadAsStringAsync();

                dynamic deserialize = JsonConvert.DeserializeObject(stringResult);
                double exchangeRate = deserialize["data"][secondary]["value"];

                CalculatedValue = exchangeRate * Amount;
                CalculatedValue = Math.Round(CalculatedValue, 2);

            }
            //catch (HttpRequestException httpRequestException)
            //{
            //    _ = new CustomizedMessageBox(httpRequestException.Message, MessageType.Error, MessageButton.Ok).ShowDialog();
            //}
            catch (ArgumentNullException nullException)
            {
                _ = new CustomizedMessageBox("Pole nie może być puste, uzupełnij lub połącz z Internetem", MessageType.Warning, MessageButton.Ok).ShowDialog();
            }
        }

        private ObservableCollection<Currency> _currencies;
        public ObservableCollection<Currency> Currencies

        {
            get { return _currencies; }
            set
            {
                _currencies = value;
                OnPropertyChanged("Currencies");
            }
        }

        private int _primaryCurrency;
        public int PrimaryCurrency
        {
            get { return _primaryCurrency; }
            set
            {
                _primaryCurrency = value;
                OnPropertyChanged("PrimaryCurrency");
            }

        }

        private double _amount;

        public double Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }

        private double _calculatedValue;
        public double CalculatedValue
        {
            get { return _calculatedValue; }
            set
            {
                _calculatedValue = value;
                OnPropertyChanged("CalculatedValue");
            }
        }

        private int _secondaryCurrency;

        public int SecondaryCurrency
        {
            get { return _secondaryCurrency; }
            set
            {
                _secondaryCurrency = value;
                OnPropertyChanged("SecondaryCurrency");
            }

        }
    }
}
