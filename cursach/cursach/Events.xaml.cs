using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace cursach
{
    /// <summary>
    /// Interaction logic for Events.xaml
    /// </summary>
    public partial class Events : Window
    {
        static SqlConnection sqlConn = new SqlConnection("Data Source=DESKTOP-52QTNQ9;Initial Catalog=MY_DB; Integrated Security=true;");
        static DataTable dT = new DataTable("Події");
        static DateOnly? up = null;
        static DateOnly? down = null;
        SqlDataAdapter? Data;

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            else
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }
        

        void UpdateDataTable()
        {
            sqlConn.Open();
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                var com = "SELECT [NUM],(convert(varchar(10), [DATE], 121)) as [DATE],[TYPE],[STATUS],[DOC_NUM],[TERITORIA] FROM [MY_DB].[dbo].[EventTable] ";
                if (up != null && down != null)
                    com += $"WHERE [DATE] <= '{down.Value.ToString("yyyy-MM-dd")}' AND [DATE] >= '{up.Value.ToString("yyyy-MM-dd")}'";
                else if (up != null)
                {
                    com += $"WHERE [DATE] >= '{up.Value.ToString("yyyy-MM-dd")}'";
                }
                else if (down != null)
                    com += $"WHERE [DATE] <= '{down.Value.ToString("yyyy-MM-dd")}'";
                com += " ORDER BY [DATE]";
                Data = new SqlDataAdapter(com, sqlConn);
                dT.Clear();
                Data.Fill(dT);
                EventsGrid.ItemsSource = dT.DefaultView;
                CCounter.Content = dT.Rows.Count;
            }
            sqlConn.Close();
        }

        public Events()
        {            
            InitializeComponent();
            UpdateDataTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var row_list = GetDataGridRows(EventsGrid); 
            foreach (DataGridRow single_row in row_list.Where(x => x.IsSelected))
            {
                try
                {
                    (new EventViev(dT.Rows[single_row.GetIndex()].Field<int>("NUM"))).Show();
                }
                catch (Exception ex)
                {
                    (new EventViev(-1)).Show();
                    //MessageBox.Show(ex.Message);
                }
            }
        }

        private void EventsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row_list = GetDataGridRows(EventsGrid);
            foreach (DataGridRow single_row in row_list.Where(x => x.IsSelected))
            {
                try { 
                    (new EventViev(dT.Rows[single_row.GetIndex()].Field<int>("NUM"))).Show();
                }
                catch (Exception ex)
                {
                    (new EventViev(-1)).Show();
                    //MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (new MainWindow()).Show();
            Close();
        }

        private void up_date_DataContextChanged(object sender, SelectionChangedEventArgs e)
        {
            if (up_date.SelectedDate != null)
                up = DateOnly.FromDateTime((DateTime)up_date.SelectedDate);
            else
                up = null; 
            UpdateDataTable();
        }
        
        private void down_date_DataContextChanged(object sender, SelectionChangedEventArgs e)
        {
            if (down_date.SelectedDate != null)
                down = DateOnly.FromDateTime((DateTime)down_date.SelectedDate);
            else
                down = null; 
            UpdateDataTable();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            (new EventViev(-1)).Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            UpdateDataTable();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var row_list = GetDataGridRows(EventsGrid);
            foreach (DataGridRow single_row in row_list.Where(x => x.IsSelected))
            {
                try
                {
                    var num = dT.Rows[single_row.GetIndex()].Field<int>("NUM");
                    var com = $"DELETE FROM [MY_DB].[dbo].[EventTable] WHERE [NUM] = {num}";
                    sqlConn.Open();
                    var cmd = new SqlCommand(com, sqlConn);
                    cmd.ExecuteNonQuery();
                    sqlConn.Close();
                    UpdateDataTable();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
