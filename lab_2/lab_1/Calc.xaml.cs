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
    /*
     <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="3*" />
            <RowDefinition></RowDefinition>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="1*"/>
            <ColumnDefinition Width="3*"/>
            <ColumnDefinition Width="1*"/>
            <ColumnDefinition Width="1*"></ColumnDefinition>
        </Grid.ColumnDefinitions>

        <Button Content="To Home" HorizontalAlignment="Center" VerticalAlignment="Center" Width="94" Height="46" Grid.Column="3" Grid.Row="1" Click="ToHome"/>
        <Grid x:Name="calc_grid" Grid.RowSpan="2" Grid.Column="1">
            <Grid.RowDefinitions>
                <RowDefinition/>
                <RowDefinition/>
                <RowDefinition/>
                <RowDefinition/>
                <RowDefinition/>
                <RowDefinition/>
            </Grid.RowDefinitions>
            <Grid.ColumnDefinitions>
                <ColumnDefinition/>
                <ColumnDefinition/>
                <ColumnDefinition/>
                <ColumnDefinition/>
            </Grid.ColumnDefinitions>
            <Button Grid.Column="3" Content="+"  Grid.Row="5" FontSize="48" Click="Button_Click_15"/>
            <Button Grid.Column="3" Grid.Row="4" Content="-"  FontSize="48" Click="Button_Click_14"/>
            <Button Grid.Column="3" Grid.Row="3" Content="×"  FontSize="48" Click="Button_Click_13"/>
            <Button Grid.Column="3" Grid.Row="2" Content="÷"  FontSize="48" Click="Button_Click_12"/>
            <Button Grid.Column="3" Grid.Row="1" Content="&lt;-"  FontSize="48" Click="Button_Click_19"/>
            <Button Grid.Column="2" Grid.Row="1" Content="c"  FontSize="48" Click="Button_Click_18"/>
            <Button Grid.Column="1" Grid.Row="1" Content="="  FontSize="48" Click="Button_Click_2"/>
            <Button Grid.Column="1" Grid.Row="2" Content="8"  FontSize="48" Click="Button_Click_10"/>
            <Button Grid.Column="0" Grid.Row="2" Content="7"  FontSize="48" Click="Button_Click_9"/>
            <Button Grid.Column="2" Grid.Row="2" Content="9"  FontSize="48" Click="Button_Click_11"/>
            <Button Grid.Column="2" Grid.Row="3" Content="6"  FontSize="48" Click="Button_Click_8"/>
            <Button Grid.Column="1" Grid.Row="3" Content="5"  FontSize="48" Click="Button_Click_7"/>
            <Button Grid.Column="1" Grid.Row="4" Content="2"  FontSize="48" Click="Button_Click_4"/>
            <Button Grid.Column="1" Grid.Row="5" Content="0"  FontSize="48" Click="Button_Click_1"/>
            <Button Grid.Column="0" Grid.Row="3" Content="4"  FontSize="48" Click="Button_Click_6"/>
            <Button Grid.Column="0" Grid.Row="4" Content="1"  FontSize="48" Click="Button_Click_3"/>
            <Button Grid.Column="2" Grid.Row="4" Content="3"  FontSize="48" Click="Button_Click_5"/>
            <Button Grid.Column="0" Grid.Row="5" Content="+/-"  FontSize="48" Click="Button_Click_17"/>
            <Button Grid.Column="2" Grid.Row="5" Content=","  FontSize="48" Click="Button_Click_16"/>
            <TextBox Grid.ColumnSpan="4" FontSize="50" TextAlignment="Right" x:Name="textBox" TextChanged="TextBox_TextChanged" KeyDown="TextBox_KeyDown" />
        </Grid>
    </Grid>
     */
    /// <summary>
    /// Логика взаимодействия для DB.xaml
    /// </summary>
    /// 

    public partial class Calc : Window
    {
        string curentval = "0";
        char sign = 'e';
        TextBox textBox = new TextBox();
        public Calc()
        {
            InitializeComponent();
            Grid grid1 = new Grid();
            var tc = new RowDefinition();
            var gridLengthConverter = new GridLengthConverter();
            tc.Height = (GridLength)gridLengthConverter.ConvertFrom("3*");
            grid1.RowDefinitions.Add(tc);
            grid1.RowDefinitions.Add(new RowDefinition());

            var tcc = new ColumnDefinition();
            tcc.Width = (GridLength)gridLengthConverter.ConvertFrom("3*");

            grid1.ColumnDefinitions.Add(new ColumnDefinition());
            grid1.ColumnDefinitions.Add(tcc);
            grid1.ColumnDefinitions.Add(new ColumnDefinition());
            grid1.ColumnDefinitions.Add(new ColumnDefinition());

            Button btn = new Button();
            btn.Content = "To Home";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Width = 94;
            btn.Height = 46;
            btn.Click += ToHome;
            Grid.SetColumn(btn, 3);
            Grid.SetRow(btn, 1);
            grid1.Children.Add(btn);
            Grid calc_grid = new Grid();
            for (int i = 0; i < 6; i++)
                calc_grid.RowDefinitions.Add(new RowDefinition());
            for (int j = 0; j < 4; j++)
                calc_grid.ColumnDefinitions.Add(new ColumnDefinition());
            Grid.SetColumn(calc_grid, 1);
            Grid.SetRowSpan(calc_grid, 2);
            grid1.Children.Add(calc_grid);
            Grid.SetColumnSpan(textBox, 4);
            textBox.FontSize = 50;
            textBox.TextAlignment = TextAlignment.Right;
            textBox.TextChanged += TextBox_TextChanged;
            textBox.KeyDown += TextBox_KeyDown;
            calc_grid.Children.Add(textBox);
            calc_grid.Children.Add(ButGenerator(3, 5, "+", (object s, RoutedEventArgs e) => Button_Sign(s, e, '+')));
            calc_grid.Children.Add(ButGenerator(3, 4, "-", (object s, RoutedEventArgs e) => Button_Sign(s, e, '-')));
            calc_grid.Children.Add(ButGenerator(3, 3, "×", (object s, RoutedEventArgs e) => Button_Sign(s, e, '*')));
            calc_grid.Children.Add(ButGenerator(3, 2, "÷", (object s, RoutedEventArgs e) => Button_Sign(s, e, '/')));
            calc_grid.Children.Add(ButGenerator(3, 1, "<-", Button_Clear));
            calc_grid.Children.Add(ButGenerator(2, 1, "c", Button_C));
            calc_grid.Children.Add(ButGenerator(0, 5, "+/-", Button_Revers));
            calc_grid.Children.Add(ButGenerator(3, 1, "<-", Button_Clear));
            calc_grid.Children.Add(ButGenerator(1, 1, "=", Button_Ecval));
           
            for(int i = 0; i < 9; i++)
            {
                //MessageBox.Show((i+1).ToString());
                var str = (i+1).ToString();
                calc_grid.Children.Add(ButGenerator(i % 3, 4 - i / 3, str ,(object s, RoutedEventArgs e) => Button_num(s, e, str)));
            }

            calc_grid.Children.Add(ButGenerator(1, 5, "0", (object s, RoutedEventArgs e) => Button_num(s, e, "0")));
            calc_grid.Children.Add(ButGenerator(2, 5, ",", (object s, RoutedEventArgs e) => Button_num(s, e, ".")));

            Xwin.Content = grid1;
            textBox.Focus();
        }
        private Button ButGenerator(int Column, int Row, string Content,RoutedEventHandler fun)
        {
            var btn = new Button();
            Grid.SetColumn(btn, Column);
            Grid.SetRow(btn, Row);
            btn.Content = Content;
            btn.Click += fun;
            btn.FontSize = 48;
            return btn;
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

        private void Button_Ecval(object sender, RoutedEventArgs e)
        {
            textBox.Text = f_calc(curentval, textBox.Text, sign);
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        static string f_calc(string a, string b, char sign)
        {
            switch (sign)
            { 
                case '+': return (double.Parse(a) + double.Parse(b)).ToString();
                case '-': return (double.Parse(a) - double.Parse(b)).ToString();
                case '*': return (double.Parse(a) * double.Parse(b)).ToString();
                case '/': return (double.Parse(a) / double.Parse(b)).ToString();
                default: return "err";
            }


        }

        private void Button_num(object sender, RoutedEventArgs e, string number)
        {
            textBox.Text += number;
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Sign(object sender, RoutedEventArgs e, char signnn)
        {
            curentval = textBox.Text;
            textBox.Text = "";
            sign = signnn;
            textBox.Focus();
        }

        private void Button_Revers(object sender, RoutedEventArgs e)
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

        private void Button_C(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            if(textBox.Text.Length != 0)
            textBox.Text = textBox.Text.Substring(0,textBox.Text.Length-1);
            textBox.Focus();
            textBox.SelectionStart = int.MaxValue;
        }
    }
}
