using System;
using System.Collections.Generic;
using System.IO;
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

namespace prac_1_2
{
    /// <summary>
    /// Interaction logic for StudyMode.xaml
    /// </summary>
    public partial class StudyMode : Window
    {
        static double stuard = 1.86;
        static long seconds;
        static int i, j;
        static int times = 3;
        static string str = "длагнитор";
        static List<List<long>> list = new List<List<long>>();
        public StudyMode()
        {
            InitializeComponent();
            list.Add(new List<long>());
            i = 0; j = 0;
        }

        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            (new MainWindow()).Show();
            Hide();
        }

        private void TextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            SymbolCount.Content = i + 1;
            var tb = (TextBox)sender;
            if ((tb.Text.Length == i + 1) && (tb.Text[i] == str[i]))
            {
                if (i == 8)
                {
                    list[j].Add(DateTime.Now.Ticks - seconds);
                    //MessageBox.Show(String.Join(" ", list[j]));
                    if (stuadrscrit(list[j]))
                    {
                        j++;
                        MessageBox.Show("yep");
                    }
                    i = 0;
                    list.Add(new List<long>());
                    if(j == times)
                    {
                        File.WriteAllText("db.txt", String.Join("\n", list.Select(x => String.Join(" ", x))));
                        (new MainWindow()).Show();
                        Hide();
                    }
                }
                else 
                {
                    if (i != 0)
                    {
                        list[j].Add(DateTime.Now.Ticks - seconds);
                    }
                    seconds = DateTime.Now.Ticks;
                    i++;
                }
            }else
            {
                list[j] = new List<long>();
                tb.Text = "";
                i = 0;
            }
        }

        private bool stuadrscrit(List<long> list1)
        {
            var y = list1.Select((_,i) => { var templ = list1.ToList(); templ.RemoveAt(i); return templ;}).ToList();
            var M = y.Select(x => x.Sum()/7.0).ToList();
            var S = y.Select((x, i) =>Math.Sqrt(x.Select(t => (t - M[i])*(t - M[i])).Sum()/6.0)).ToList();
            var t = list1.Select((x,i) => Math.Abs((x- M[i])/(S[i]/Math.Sqrt(7.0)))).ToList();
            return t.Average() > stuard;
        }

        private void CountProtection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            times =int.Parse(((ComboBoxItem)((ComboBox)sender).SelectedValue).Content.ToString());
        }
    }
}
