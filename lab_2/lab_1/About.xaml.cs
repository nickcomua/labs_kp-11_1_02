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

namespace lab_1
{
    /*
     <Grid>
        <Grid.RowDefinitions>
            <RowDefinition></RowDefinition>
            <RowDefinition></RowDefinition>
            <RowDefinition></RowDefinition>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition></ColumnDefinition>
            <ColumnDefinition></ColumnDefinition>
            <ColumnDefinition></ColumnDefinition>
            <ColumnDefinition></ColumnDefinition>
        </Grid.ColumnDefinitions>

        <Button Content="To Home" HorizontalAlignment="Center" VerticalAlignment="Center" Width="94" Height="46" Grid.Column="3" Grid.Row="2" Click="ToHome"/>
        <Label Content="Powerd by Korniichuk Mykola KP-11" HorizontalAlignment="Left" VerticalAlignment="Center" FontSize="46" Height="182" Width="800" Grid.ColumnSpan="4" Grid.Row="1" FontWeight="Bold"/>
    </Grid>
     */
    /// <summary>
    /// Логика взаимодействия для DB.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            Label label = new Label();
            // <Label Content="Powerd by Korniichuk Mykola KP-11" HorizontalAlignment="Left" VerticalAlignment="Center" FontSize="46" Height="182" Width="800" Grid.ColumnSpan="4" Grid.Row="1" FontWeight="Bold"/>
            label.Content = "Powerd by Korniichuk Mykola KP-11";
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.FontSize = 46;
            label.Height = 182;
            label.Width = 800;
            Grid.SetColumn(label, 0); 
            Grid.SetRow(label, 1);
            Grid.SetColumnSpan(label, 4);
            grid.Children.Add(label);
            //<Button Content="To Home" HorizontalAlignment="Center" VerticalAlignment="Center" Width="94" Height="46" Grid.Column="3" Grid.Row="2" Click="ToHome"/>

            Button button = new Button();
            button.Content = "To Home";
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Width = 94;
            button.Height = 46;
            button.Click += ToHome;
            Grid.SetColumn(button, 3);
            Grid.SetRow(button, 2);
            grid.Children.Add(button);
            Awin.Content = grid;

        }

        private void ToHome(object sender, RoutedEventArgs e)
        {
            var mw = new MainWindow();
            mw.Show();
            Hide(); 

        }
    }
}
