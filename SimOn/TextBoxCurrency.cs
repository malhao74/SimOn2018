using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimOn
{
    public class TextBoxCurrency : TextBox
    {
        public TextBoxCurrency()
            : base()
        {
            KeyDown += TextBoxCurrency_KeyDown;
            GotFocus += TextBoxCurrency_GotFocus;
            LostFocus += TextBoxCurrency_LostFocus;
            TextChanged += TextBoxCurrency_TextChanged;
        }

        private void TextBoxCurrency_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxCurrency_LostFocus(sender, e);
        }

        public double GetValue()
        {
            string text = Text.Replace(" ", "").Replace("€", "").Replace(".", ""); //.Replace(",",".");
            return Convert.ToDouble("0" + text);
        }

        #region Metodos Privados
        private void TextBoxCurrency_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            double valor = GetValue(); // Convert.ToDouble("0" + Text);
            Text = valor.ToString("C");
        }

        private void TextBoxCurrency_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            Text = GetValue().ToString(); // Text.Replace(" ","").Replace("€","");
        }

        private void TextBoxCurrency_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Key key = e.Key;

            if ((key >= Key.D0 && key <= Key.D9) || (key >= Key.NumPad0 && key <= Key.NumPad9))
            {
                return;
            }

            if (key == Key.OemComma && Text.Contains(",") == false)
            {
                return;
            }

            if (key == Key.Tab)
            {
                return;
            }

            e.Handled = true;
        }
        #endregion
    }
}
