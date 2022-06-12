using Intermix.Commands;
using Intermix.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Intermix.ViewModels.CurrencyConverter
{
    public class CurrencyConverterPageViewModel : BaseViewModel
    {

        private const string API_KEY = "fc3685a0-39a4-11ec-87f9-a764dd5862af";
        private const string BASE_HTTPS_URL = @"https://free.currencyconverterapi.com";

        public ICommand Convert { get; set; }
        public CurrencyConverterPageViewModel()
        {
            Convert = new RelayCommand(o => GetExchangeRate(PrimaryCurrency, SecondaryCurrency), o => true);
        }

        public async Task<string> GetExchangeRate(string from, string to)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BASE_HTTPS_URL);
                    var response = await client.GetAsync($"/api/v6/convert?q={from}_{to}&compact=y");
                    var stringResult = await response.Content.ReadAsStringAsync();

                    var dictResult = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(stringResult);
                    return dictResult[$"{from}_{to}"]["val"];


                }
                catch (HttpRequestException httpRequestException)
                {
                    Console.WriteLine(httpRequestException.StackTrace);
                    return "Error calling API. Please do manual lookup.";
                }
            }
        }

        private string _primaryCurrency;

        public string PrimaryCurrency
        {
            get { return _primaryCurrency; }
            set { _primaryCurrency = value;
                OnPropertyChanged("PrimaryCurrency");
            }
        
        }

        private string _secondaryCurrency;

        public string SecondaryCurrency
        {
            get { return _secondaryCurrency; }
            set
            {_secondaryCurrency = value;
             OnPropertyChanged("SecondaryCurrency");
            }

        }

        private bool _isConverting;

        public bool IsConverting
        {
            get { return _isConverting; }
            set { _isConverting = value;
                OnPropertyChanged("IsConverting");
            }
        }



    }
}
