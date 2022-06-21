using Intermix.Commands;
using Intermix.ViewModels.Base;
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
    #region Model

    public class CurrenciesModel
    {
        public string ShortName { get; set; }
        public string LongName { get; set; }
    }

    #endregion
    public class CurrencyConverterPageViewModel : BaseViewModel
    {
        #region CONST
        private const string BASE_HTTPS_URL = @"https://api.currencyapi.com/v3/latest?";
        private const string CURRENCY_FROM = "currencies=";
        private const string CURRENCY_TO = "&base_currency=";
        private const string API_KEY = "apikey=fc3685a0-39a4-11ec-87f9-a764dd5862af&";
        #endregion

        public ICommand ConvertCommand { get; set; }
        public CurrencyConverterPageViewModel()
        {
            ConvertCommand = new RelayCommand(o => ConvertCurrencies(), o => true);

            LoadMethodAsync();
        }

        private async Task LoadMethodAsync()
        {
            List<List<string>> tableData = new(await GetCurrencies());
            List<string> tableValues = new();

            tableValues = tableData.SelectMany(x => x).ToList();

            Currencies = new ObservableCollection<CurrenciesModel>();

            for (int i = 0; i < tableValues.Count; i += 2)
            {
                string x = tableValues[i];
                string y = tableValues[i + 1];

                Currencies.Add(new CurrenciesModel { ShortName = x, LongName = y });
            }
        }

        public async Task<List<List<string>>> GetCurrencies()
        {
            string url = @"https://currencyapi.com/docs/currency-list#currency-list";
            using (var client = new HttpClient())
            {
                var html = await client.GetStringAsync(url);
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
        }

        private async void ConvertCurrencies()
        {
            IsConverting = true;
            using (var client = new HttpClient())
            {
                try
                {
                    string primary = Currencies.Select(x => x.ShortName).ElementAt(PrimaryCurrency);
                    string secondary = Currencies.Select(x => x.ShortName).ElementAt(SecondaryCurrency);

                    var response = await client.GetAsync(BASE_HTTPS_URL + API_KEY + CURRENCY_FROM + secondary + CURRENCY_TO + primary);
                    var stringResult = await response.Content.ReadAsStringAsync();

                    dynamic deserialize = JsonConvert.DeserializeObject(stringResult);
                    double exchangeRate = deserialize["data"][secondary]["value"];

                    CalculatedValue = exchangeRate * Amount;
                    CalculatedValue = Math.Round(CalculatedValue, 2);

                }
                catch (HttpRequestException httpRequestException)
                { }
                catch (NullReferenceException e)
                { }

            }
            IsConverting = false;
            HasConverted = true;

        }

        private ObservableCollection<CurrenciesModel> _currencies;
        public ObservableCollection<CurrenciesModel> Currencies

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

        private bool _isConverting;

        public bool IsConverting
        {
            get { return _isConverting; }
            set
            {
                _isConverting = value;
                OnPropertyChanged("IsConverting");
            }
        }

        private bool _hasConverted = false;
        public bool HasConverted
        {
            get { return _hasConverted; }
            set
            {
                _hasConverted = value;
                OnPropertyChanged("HasConverted");


            }
        }



    }
}
