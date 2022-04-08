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

namespace lab_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void gotoDB(object sender, RoutedEventArgs e)
        {
            (new DB()).Show();
            Hide();
        }

        private void gotoGame(object sender, RoutedEventArgs e)
        {
            (new Game()).Show();
            Hide();
        }

        private void gotoCalc(object sender, RoutedEventArgs e)
        {
            (new Calc()).Show();
            Hide();

        }

        private void gotoAbout(object sender, RoutedEventArgs e)
        {
            (new About()).Show();
            Hide();
        }
    }
}
