using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    /// Interaction logic for ProtectionMode.xaml
    /// </summary>
    public partial class ProtectionMode : Window
    {

        static double stuard = 2.306;
        static double fisher = 3.44;
        static long seconds;
        static int i, j, n, p1 = 0, p2 = 0;
        static int times = 3;

        static string str = "длагнитор";
        static List<long> list = new List<long>();
        static List<List<long>> check_list = new List<List<long>>();
        public ProtectionMode()
        {
            InitializeComponent();
            i = 0;j = 0;
            check_list = File.ReadAllLines("DB.txt").Select(x => x.Split(" ").Select(t => long.Parse(t)).ToList()).ToList();
            n = check_list.Count;
            P1Field.Content = 0;
            P2Field.Content = 0;
        }

        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            (new MainWindow()).Show();
            Hide();
        }

        private void InputField_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            SymbolCount.Content = i + 1;
            var tb = (TextBox)sender;
            if ((tb.Text.Length == i + 1) && (tb.Text[i] == str[i]))
            {
                if (i == 8)
                {
                    list.Add(DateTime.Now.Ticks - seconds);
                    float f = fisher_test(list, check_list, n);
                    DispField.Content = f;
                    float f2 = stuard_test(list, check_list, n);
                    StatisticsBlock.Content = f2;
                    j++;
                    i = 0;
                    var result = MessageBox.Show("ви є користувачом?", "1", MessageBoxButton.YesNo);
                    var flag1 = (result == MessageBoxResult.Yes);
                    result = MessageBox.Show("ви пройшли аутиніфікацію?", "2", MessageBoxButton.YesNo);
                    var flag2 = (result == MessageBoxResult.Yes);
                    if (!flag1 && flag2)
                        p1++;
                    P2Field.Content = (double)p1/j;
                    if (flag1 && !flag2)
                        p2++;
                    P1Field.Content = (double)p2 /j;
                    list = new List<long>();
                    if (j == times)
                    {
                        (new MainWindow()).Show();
                        Hide();
                    }
                }
                else
                {
                    if (i != 0)
                    {
                        list.Add(DateTime.Now.Ticks - seconds);
                    }
                    seconds = DateTime.Now.Ticks;
                    i++;
                }
            }
            else
            {

                list = new List<long>();
                tb.Text = "";
                i = 0;
            }
        }

        private float stuard_test(List<long> list1, List<List<long>> check_list, int n)
        {
            int counter = 0;
            foreach(var list2 in check_list)
            {
                var y1 = list1.Select((_, i) => { var templ = list1.ToList(); templ.RemoveAt(i); return templ; }).ToList();
                var M1 = y1.Select(x => x.Sum() / 7.0).ToList();
                var S1 = y1.Select((x, i) => Math.Sqrt(x.Select(t => (t - M1[i]) * (t - M1[i])).Sum() / 6.0)).ToList();
                var y2 = list2.Select((_, i) => { var templ = list2.ToList(); templ.RemoveAt(i); return templ; }).ToList();
                var M2 = y2.Select(x => x.Sum() / 7.0).ToList();
                var S2 = y2.Select((x, i) => Math.Sqrt(x.Select(t => (t - M2[i]) * (t - M2[i])).Sum() / 6.0)).ToList();
                var S = S1.Select((x, i) => Math.Sqrt((x * x + S2[i] * S2[i]) * 7.0 / 17.0)).ToList();

                if (M1.Select((x, i) => ((double)Math.Abs(x - M2[i])) / S[i] * 2.0).All(x => x < stuard))
                {
                    counter++;
                }
            }
            return (float)(counter) /n;

        }

        private float fisher_test(List<long> list1, List<List<long>> check_list, int n)
        {
            //var list2 = check_list[0];
            //MessageBox.Show(String.Join(" ", list1.Select((x, i) => (double)Math.Max(x * x, list2[i] * list2[i]) / Math.Min(x * x, list2[i] * list2[i])).ToArray()));
            return (float)check_list.Select(list2 =>
                list1.Select((x, i) => (double)Math.Max(x*x, list2[i] * list2[i]) / Math.Min(x*x, list2[i] * list2[i]))
                    ).Count(x => x.All(x => x < fisher))
                        / n;
        }

        private void CountProtection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            times = int.Parse(((ComboBoxItem)((ComboBox)sender).SelectedValue).Content.ToString());
        }
    }
}
