using PrimeNumbersGenerator;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace PrimeNumberGenerator
{
	public partial class MainWindow : Window
	{
        private readonly string _defaultFirstValueText = "First value";
        private readonly string _defaultLastValueText = "Last value";
        private Generator _generator;
        private bool _isButtonLocked;


        public MainWindow()
		{
			InitializeComponent();
            _generator = new Generator();
		}

        private void TxtFirstValue_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtFirstValue.Text.Equals(_defaultFirstValueText))
            {
                txtFirstValue.Text = "";
                txtFirstValue.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }

        private void TxtFirstValue_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFirstValue.Text.Equals(""))
            {
                txtFirstValue.Text = _defaultFirstValueText;
                txtFirstValue.Foreground = new SolidColorBrush(Color.FromRgb(126, 126, 126));
            }
        }

        private void TxtLastValue_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtLastValue.Text.Equals(_defaultLastValueText))
            {
                txtLastValue.Text = "";
                txtLastValue.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }

        private void TxtLastValue_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtLastValue.Text.Equals(""))
            {
                txtLastValue.Text = _defaultLastValueText;
                txtLastValue.Foreground = new SolidColorBrush(Color.FromRgb(126, 126, 126));
            }
        }

        private void TxtFirstValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtLastValue.Focus();
            }
        }

        private void TxtLastValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtLastValue.Focus();
            }
        }

        private void TxtFirstValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
             

        private void TxtLastValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtFirstValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void TxtLastValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void tgbParallel_Checked(object sender, RoutedEventArgs e)
        {
            tgbParallel.Content = "Par";
        }

        private void tgbParallel_Unchecked(object sender, RoutedEventArgs e)
        {
            tgbParallel.Content = "Seq";
        }


        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!_isButtonLocked)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                _isButtonLocked = true;
                lsbPrimeNumbers.Items.Clear();
                lblTimeElapsed.Content = "";
                spnGenerating.Visibility = Visibility.Visible;

                long firstValue = long.Parse(txtFirstValue.Text);
                long lastValue = long.Parse(txtLastValue.Text);
                List<long> primeNumbers = new List<long>();
                if ((bool)tgbParallel.IsChecked)
                {
                    primeNumbers = await _generator.GetPrimesParallel(firstValue, lastValue);
                    primeNumbers.Sort();
                }
                else
                {
                    primeNumbers = await _generator.GetPrimesSequential(firstValue, lastValue);
                }
                for (int i = 0; i < primeNumbers.Count; i++)
                {
                    lsbPrimeNumbers.Items.Add(primeNumbers[i]);
                }

                stopwatch.Stop();
                spnGenerating.Visibility = Visibility.Collapsed;
                lblTimeElapsed.Content = $"Time Elapsed: {stopwatch.ElapsedMilliseconds / 1000.0f} seconds";
                _isButtonLocked = false;
            }
        }
    }
}
