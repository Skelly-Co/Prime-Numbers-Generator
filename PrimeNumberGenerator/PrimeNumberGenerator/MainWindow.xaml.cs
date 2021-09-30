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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrimeNumberGenerator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private readonly string _defaultFirstValueText = "First value";
        private readonly string _defaultLastValueText = "Last value";

        public MainWindow()
		{
			InitializeComponent();
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

        private void TxtLastValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxToProperCase(txtLastValue);
        }
        private void TextBoxToProperCase(TextBox textBox)
        {
            if (!textBox.Text.Equals(""))
            {
                char[] v = textBox.Text.ToCharArray();
                string s = v[0].ToString().ToUpper();
                for (int b = 1; b < v.Length; b++)
                    s += v[b].ToString().ToLower();
                textBox.Text = s;
                textBox.Select(textBox.Text.Length, 0);
            }
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


        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            lsbPrimeNumbers.Items.Add(1);
        }
    }
}
