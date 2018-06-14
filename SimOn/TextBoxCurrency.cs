using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace SimOn
{
    public class TextBoxCurrency : TextBox
    {
        public TextBoxCurrency()
            : base()
        {
            GotFocus += TextBoxCurrency_GotFocus;
            LostFocus += TextBoxCurrency_LostFocus;
            KeyDown += TextBoxCurrency_KeyDown;
        }

        #region Metodos privados
        // Apenas permitir o input de numeros ou uma virgula
        private void TextBoxCurrency_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            System.Windows.Input.Key key = e.Key;
            if (key == System.Windows.Input.Key.Enter)
            {
                // TODO: Quando carregarem no enter passar para tab.

            }
            if (key >= System.Windows.Input.Key.D0 && key <= System.Windows.Input.Key.D9 )
            { return; }
            if (key == System.Windows.Input.Key.OemComma)
            {
                if (Text.IndexOf(',') > -1)
                { e.Handled = true; }
                return;
            }
            if (key == System.Windows.Input.Key.Delete)
            { return; }

            e.Handled = true;
            return;
        }

        // Depois do campo ter sido editado volta a formatar em moeda.
        private void TextBoxCurrency_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            double valor = Convert.ToDouble("0"+Text);
            Text = valor.ToString("C");
        }

        // Quando o campo começa a ser editado, passa para valor.
        private void TextBoxCurrency_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Text != "")
            {
                string cleanText = Text.Replace(" ", "").Replace("€", "");
                double valor = Convert.ToDouble(cleanText);
                Text = valor.ToString();
            }
        }
        #endregion
    }
}
