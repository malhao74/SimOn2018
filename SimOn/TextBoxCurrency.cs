using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;

namespace SimOn
{
    /// <summary>
    /// Custome textbox to display currency formated values and allow input 
    /// </summary>
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

        /// <summary>
        /// Return the Text property as a double value
        /// </summary>
        /// <returns></returns>
        public double ToDouble()
        {
            string text = Text.Trim();
            if (String.IsNullOrEmpty(text))
            {
                text = "0";
            }
            return Double.Parse(text,NumberStyles.Currency, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Takes a double and format it to currency and updates de Text properties with that value 
        /// </summary>
        /// <param name="value"></param>
        public void FromDouble(double value)
        {
            Text = value.ToString("C", CultureInfo.CurrentCulture);
        }

        #region Events
        private void TextBoxCurrency_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TextBoxCurrency_LostFocus(sender, e);
        }

        private void TextBoxCurrency_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            double value = ToDouble();
            FromDouble(value);
        }

        private void TextBoxCurrency_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            double value = ToDouble();
            Text = value.ToString("F2", CultureInfo.CurrentCulture); 
        }

        private void TextBoxCurrency_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Key key = e.Key;

            if ((key >= Key.D0 && key <= Key.D9) || (key >= Key.NumPad0 && key <= Key.NumPad9))
            {
                return;
            }
            
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if ((key == Key.OemComma || key == Key.OemPeriod) && Text.Contains(decimalSeparator) == false)
            {
                int caretPosition = SelectionStart;
                Text = Text.Insert(caretPosition, decimalSeparator);
                SelectionStart = caretPosition + 1;
                SelectionLength = 0;
                e.Handled = true;
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
