using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab_1
{
    /// <summary>
    /// Логика взаимодействия для DB.xaml
    /// </summary>
    /// 
  
    public partial class Calc : Window
    {
        string curentval = "0";
        char sign = 'e';
        public Calc()
        {
            InitializeComponent();
            textBox.Focus();
        }
        private void ToHome(object sender, RoutedEventArgs e)
        {
            var mw = new MainWindow();
            mw.Show();
            Hide(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show("fdg");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textBox.Text += "0";
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                textBox.Text = f_calc(curentval, textBox.Text, sign);
                textBox.SelectionStart = int.MaxValue;
                e.Handled = true;
            }
            else if (e.Key == Key.Subtract)
            {
                e.Handled = true;
                curentval = textBox.Text;
                textBox.Text = "";
                sign = '-';
            }
            else if (e.Key == Key.Multiply)
            {
                e.Handled = true;
                curentval = textBox.Text;
                textBox.Text = "";
                sign = '*';
            }
            else if (e.Key == Key.Divide)
            {
                e.Handled = true;
                curentval = textBox.Text;
                textBox.Text = "";
                sign = '/';
            }
            else if (e.Key == Key.Add)
            {
                e.Handled = true;
                curentval = textBox.Text;
                textBox.Text = "";
                sign = '+';
            }
            else if (e.Key < Key.D0 || e.Key > Key.D9 || e.Key == Key.Decimal)
                e.Handled = true;
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            textBox.Text = f_calc(curentval, textBox.Text, sign);
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        static string f_calc(string a, string b, char sign)
        {
            switch (sign)
            { 
                case '+' : return (double.Parse(a) + double.Parse(b)).ToString();
                case '-': return (double.Parse(a) - double.Parse(b)).ToString();
                case '*': return (double.Parse(a) * double.Parse(b)).ToString();
                case '/': return (double.Parse(a) / double.Parse(b)).ToString();
                default: return "err";
            }


        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            textBox.Text += "1";
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            textBox.Text += "2";
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            textBox.Text += "3";
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            textBox.Text += "4";
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            textBox.Text += "5";
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            textBox.Text += "6";
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            textBox.Text += "7";
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            textBox.Text += "8";
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            textBox.Text += "9";
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            curentval = textBox.Text;
            textBox.Text = "";
            sign = '/';
            textBox.Focus();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            curentval = textBox.Text;
            textBox.Text = "";
            sign = '*';
            textBox.Focus();
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            curentval = textBox.Text;
            textBox.Text = "";
            sign = '-';
            textBox.Focus();
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            curentval = textBox.Text;
            textBox.Text = "";
            sign = '+';
            textBox.Focus();
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            textBox.Text += ",";
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            if(textBox.Text[0] == '-')
            {
                textBox.Text = textBox.Text.Substring(1);
            }
            else
            {
                textBox.Text = "-"+textBox.Text;
            }
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            textBox.Text = textBox.Text.Substring(0,textBox.Text.Length-1);
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }
    }
}
