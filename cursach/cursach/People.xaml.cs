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
    /// Interaction logic for People.xaml
    /// </summary>
    public partial class People : Window
    {
        static SqlConnection sqlConn = new SqlConnection("Data Source=DESKTOP-52QTNQ9;Initial Catalog=MY_DB; Integrated Security=true;");
        static DataTable dT = new DataTable("Люди");
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
                var com = "SELECT * FROM [MY_DB].[dbo].[PeopleTable]";
                Data = new SqlDataAdapter(com, sqlConn);
                dT.Clear();
                Data.Fill(dT);
                PeopleGrid.ItemsSource = dT.DefaultView; 
            }
            sqlConn.Close();
        }


        public People()
        {
            InitializeComponent();
            UpdateDataTable();
        }

        private void PeopleGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row_list = GetDataGridRows(PeopleGrid);
            foreach (DataGridRow single_row in row_list.Where(x => x.IsSelected))
            {
                try
                {
                    (new PersonWindow(dT.Rows[single_row.GetIndex()].Field<int>("ID"))).Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);                    
                    (new PersonWindow(-1)).Show();
                    //MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var row_list = GetDataGridRows(PeopleGrid);
            foreach (DataGridRow single_row in row_list.Where(x => x.IsSelected))
            {
                try
                {
                    var num = dT.Rows[single_row.GetIndex()].Field<int>("NUM");
                    var com = $"DELETE FROM [MY_DB].[dbo].[PeopleTable] WHERE [NUM] = {num}";
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var row_list = GetDataGridRows(PeopleGrid);
            foreach (DataGridRow single_row in row_list.Where(x => x.IsSelected))
            {
                try
                {
                    (new PersonWindow(dT.Rows[single_row.GetIndex()].Field<int>("ID"))).Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    (new PersonWindow(-1)).Show();
                    //MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            (new PersonWindow(-1)).Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            (new MainWindow()).Show();
            Close();
        }
    }
}
