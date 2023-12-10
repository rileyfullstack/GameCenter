using GameCenter.Projects.CurrencyConvertor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameCenter.Projects.CurrencyConvertor
{
    public partial class CurrencyConvertorView : Window
    {
        private CurrencyService _currencyService;
        private Dictionary<string, double> _exchangeRates;

        public CurrencyConvertorView()
        {
            InitializeComponent();
            _currencyService = new CurrencyService();
            LoadCurrencies(); 

        }

        private async void LoadCurrencies()
        {
            try
            {
                //We need to excute the method await _currencyService.GetExchangeRatesAsync()
                _exchangeRates = await _currencyService.GetExchangeRatesAsync();
                //get string[] of the keys of the dictionary
                string[] currencies = _exchangeRates.Keys.ToArray();
                //set the ItemsSource of the combo boxes to the string array of the currencies names
                FromCurrencyComboBox.ItemsSource = currencies;
                ToCurrencyComboBox.ItemsSource = currencies;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading currencies: {ex.Message}");
            }
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            //we need to get - the selected currencies by the user
            string fromCurrency = FromCurrencyComboBox.SelectedItem.ToString();
            string toCurrency = ToCurrencyComboBox.SelectedItem.ToString();
            //we need to get - the amount to convert 
            double amount = double.Parse(AmountTextBox.Text);
            //we need  2 values from the dict according to the selected items in the combo boxes
            double baseToFromRate = _exchangeRates[fromCurrency];
            double baseToToRate = _exchangeRates[toCurrency];
            double convertedAmount = (baseToToRate / baseToFromRate) * amount;
            txtResult.Text = $"Converted Amount: {amount} {fromCurrency} is {convertedAmount:0.00} {toCurrency}";
        }
    }

}
