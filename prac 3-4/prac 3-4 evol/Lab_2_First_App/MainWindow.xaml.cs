using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lab_2_First_App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static DispatcherTimer dT;
        static int Radius = 30;
        static int PointCount = 5;
        static Polygon myPolygon = new Polygon();
        static List<Ellipse> EllipseArray = new List<Ellipse>();
        static PointCollection pC = new PointCollection();

        public MainWindow()
        {
            dT = new DispatcherTimer();

            InitializeComponent();
            InitPoints();
            InitPolygon();

            dT = new DispatcherTimer();
            dT.Tick += new EventHandler(OneStep);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 1000);
        }

        private void InitPoints()
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();

            for (int i = 0; i < PointCount; i++)
            {
                Point p = new Point();

                p.X = rnd.Next(Radius, (int)(0.75 * MainWin.Width) - 3 * Radius);
                p.Y = rnd.Next(Radius, (int)(0.90 * MainWin.Height - 3 * Radius));
                pC.Add(p);
            }

            for (int i = 0; i < PointCount; i++)
            {
                Ellipse el = new Ellipse();

                el.StrokeThickness = 2;
                el.Height = el.Width = Radius;
                el.Stroke = Brushes.Black;
                el.Fill = Brushes.LightBlue;
                EllipseArray.Add(el);
            }
        }

        private void InitPolygon()
        {
            myPolygon.Stroke = System.Windows.Media.Brushes.Black;
            myPolygon.StrokeThickness = 2;
        }

        private void PlotPoints()
        {
            for (int i = 0; i < PointCount; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius / 2);
                MyCanvas.Children.Add(EllipseArray[i]);
            }
        }


        private void PlotWay(int[] BestWayIndex)
        {
            PointCollection Points = new PointCollection();

            for (int i = 0; i < BestWayIndex.Length; i++)
                Points.Add(pC[BestWayIndex[i]]);

            myPolygon.Points = Points;
            MyCanvas.Children.Add(myPolygon);
        }

        private void VelCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;

            dT.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt16(item.Content));
        }

        private void StopStart_Click(object sender, RoutedEventArgs e)
        {
            arr_of_way.Clear();
            if (dT.IsEnabled)
            {
                dT.Stop();
                NumElemCB.IsEnabled = true;
            }
            else
            {
                NumElemCB.IsEnabled = false;
                dT.Start();
            }
        }

        private void NumElemCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;

            PointCount = Convert.ToInt32(item.Content);
            InitPoints();
            InitPolygon();
        }

        private void OneStep(object sender, EventArgs e)
        {
            MyCanvas.Children.Clear();
            //InitPoints();
            PlotPoints();
            PlotWay(GetBestWay());
        }

        private double Calc(List<int> arr)
        {
            double sum = 0;
            for (int i = 1; i < PointCount; i++)
            {
                sum += way_of_two(pC[arr[i - 1]], pC[arr[i]]);
            }
            return sum;
        }
        private double way_of_two(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(a.X - b.X), 2) + Math.Pow(Math.Abs(a.Y - b.Y), 2));
        }
        Random rnd = new Random();
        private List<List<int>> crossing(List<int> a, List<int> b)
        {
            var result = new List<List<int>>();
            int p1 = rnd.Next(1, 14), p2 = rnd.Next(1, 14);
            var crossbig1 = a.Take(p1).Concat(b.Skip(p2)).Concat(a.Skip(p1)).Concat(b.Take(p2)).ToList();
            var crosobig2 = new List<int>(crossbig1);
            crossbig1.Reverse();
            for(int i = 0; i < PointCount; i++)
            {
                crossbig1.Remove(i);
                crosobig2.Remove(i);
            }
           // MessageBox.Show(crossbig1.Count().ToString());
            result.Add(mutate(crossbig1));
            result.Add(mutate(crosobig2));
            return result;
        }
        private List<int> mutate(List<int> a)
        {
            if (rnd.NextDouble() < 0.7)
                return a;
            int p1 = rnd.Next(0, PointCount-1), p2 = rnd.Next(p1, PointCount-1);
            var temp = a.Skip(p1).Take(p2 - p1).Reverse();
            return a.Take(p1).Concat(temp).Concat(a.Skip(p2)).ToList();
        }

        static List<List<int>> arr_of_way = new List<List<int>>();
        private int[] GetBestWay()
        {
            if (arr_of_way.Count == 0)
            {
                var way = Enumerable.Range(0, PointCount).ToList();
                for (int i = 0; i < 30; i++)
                {
                    arr_of_way.Add(way.OrderBy(x => rnd.Next()).ToList());
                }
            }
            //MessageBox.Show("o");
            var arr_of_way2 = arr_of_way.OrderBy(x => Calc(x)).Take(15).ToList();
            arr_of_way.Clear();
            for(int i = 1; i < 15; i++)
            {
                arr_of_way.AddRange(crossing(arr_of_way2[0],arr_of_way2[i]));
            }
            //arr_of_way.AddRange(crossing(arr_of_way2[2], arr_of_way2[1]));
            arr_of_way.Add(arr_of_way2[0]);
            arr_of_way.Add(arr_of_way2[1]);
            lable.Content = Calc(arr_of_way2[0]);
            return arr_of_way2[0].ToArray();
        }
    }
}