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
        private int[] GetBestWay()
        {
            var ans = new List<int>();
            var pCC = pC.Select((x,y) => (x,y)).ToList();
            while(pCC.Count > 1) { 
                var p = pCC[0];
                pCC.Remove(p);
                ans.Add(p.y);
                p = pCC.OrderBy(x => way_of_two(p.x, x.x)).First();
            }
            ans.Add(pCC[0].y);
            lable.Content = Calc(ans);
            return ans.ToArray();
        }
    }
}