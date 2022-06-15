using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EventViev.xaml
    /// </summary> 

    public partial class EventViev : Window
    {
        
        static int id = -1;
        static DataTable dTP = new DataTable("Люди");
        static DataTable dTPA = new DataTable("Всі люди");
        static DataTable dTE = new DataTable("Подія");
        static SqlConnection sqlConn = new SqlConnection("Data Source=DESKTOP-52QTNQ9;Initial Catalog=MY_DB; Integrated Security=true;");
        void UpdateDate()
        {
            if (sqlConn.State == ConnectionState.Closed)
                sqlConn.Open();
            SqlCommand sqlCmd = new SqlCommand($"SELECT [NUM],(convert(varchar(10), [DATE], 121)) as [DATE],[TYPE],[STATUS],[DOC_NUM],[TERITORIA] FROM [MY_DB].[dbo].[EventTable] where [NUM]={id}", sqlConn);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd);
            dTE.Clear();
            sqlDataAdapter.Fill(dTE);
            if (dTE.Rows.Count == 1)
            {
                numBox.Text = Convert.ToString(dTE.Rows[0][0]);
                dateBox.Text = Convert.ToString(dTE.Rows[0][1]);
                type_chouse.Text = Convert.ToString(dTE.Rows[0][2]);
                status_chouse.Text = Convert.ToString(dTE.Rows[0][3]);
                if (Convert.ToString(dTE.Rows[0][3]) == "Прийнята")
                {
                    numSpBox.IsEnabled = true;
                    adress_box.IsEnabled = true;
                    numSpBox.Text = dTE.Rows[0][4].ToString();
                    adress_box.Text = Convert.ToString(dTE.Rows[0][5]);
                }
                else
                {
                    numSpBox.IsEnabled = false;
                    adress_box.IsEnabled = false;
                    numSpBox.Text = "";
                    adress_box.Text = "";
                }
                
            }
            sqlDataAdapter = new SqlDataAdapter($"SELECT [PeopleTable].* ,[SuspicionTable].[SUS] FROM [MY_DB].[dbo].[PeopleTable] INNER JOIN [MY_DB].[dbo].[SuspicionTable] ON [PeopleTable].[ID] = [SuspicionTable].[PERSON] WHERE [SuspicionTable].[NUM] =  {id}", sqlConn);
            dTP.Clear();
            sqlDataAdapter.Fill(dTP);
            PeopleTable.ItemsSource = dTP.DefaultView;
            dTPA.Clear();
            if (add_people_chouse.Items.Count > 0) 
                add_people_chouse.Items.Clear();
            sqlDataAdapter = new SqlDataAdapter($"SELECT * FROM [MY_DB].[dbo].[PeopleTable]", sqlConn);
            sqlDataAdapter.Fill(dTPA);
            foreach (DataRow i in dTPA.Rows)
            {
                add_people_chouse.Items.Add($"{i[0]} {i[1]} {i[2]} {i[3]}");
            }
            sqlConn.Close();
        }
        public EventViev(int n)
        {
            if (sqlConn.State == ConnectionState.Open)
                sqlConn.Close();
            InitializeComponent();
            type_chouse.Items.Add("Наїзд на пішохода");
            type_chouse.Items.Add("Пожежа в автомобілі");
            type_chouse.Items.Add("Падіння у воду");
            type_chouse.Items.Add("Перекидання");
            type_chouse.Items.Add("Зіткнення");
            status_chouse.Items.Add("Відхилена");
            status_chouse.Items.Add("Прийнята");
            status_chouse.Items.Add("В обробці");
            sus_chouse.Items.Add("Винуватець");
            sus_chouse.Items.Add("Підозрюваний");
            sus_chouse.Items.Add("Потерпілий");
            sus_chouse.Items.Add("Свідок");
            if (n == -1)
            {
                id = newevent();
                UpdateDate();
            }
            else
            {
                id = n;
                try { 
                UpdateDate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private int newevent()
        {
            if (sqlConn.State == ConnectionState.Closed)
                sqlConn.Open();
            SqlCommand sqlCmd = new SqlCommand($"INSERT INTO [MY_DB].[dbo].[EventTable] ([DATE],[TYPE],[STATUS]) VALUES (GETDATE(),N'Наїзд на пішохода',N'В обробці')", sqlConn);
            sqlCmd.ExecuteNonQuery();
            sqlCmd = new SqlCommand($"SELECT MAX([NUM]) FROM [MY_DB].[dbo].[EventTable]", sqlConn);
            int id = (int)sqlCmd.ExecuteScalar();
            sqlConn.Close();
            return id;
        }

        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void dateBox_DataContextChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlCommand sqlCmd = new SqlCommand($"UPDATE [MY_DB].[dbo].[EventTable] SET [DATE] = '{dateBox.SelectedDate.Value:yyyy-MM-dd}' WHERE [NUM] = {id}", sqlConn);
            if (dateBox.SelectedDate.Value.ToString("yyyy - MM - dd") != Convert.ToString(dTE.Rows[0][1]))
            {
                try
                {
                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();
                    sqlConn.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    if (sqlConn.State == ConnectionState.Open)
                        sqlConn.Close();
                }
                UpdateDate();
            }
        }

        private void type_chouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlCommand sqlCmd = new SqlCommand($"UPDATE [MY_DB].[dbo].[EventTable] SET [TYPE] = N'{type_chouse.SelectedItem}' WHERE [NUM] = {id}", sqlConn);
            if (type_chouse.SelectedItem.ToString() != Convert.ToString(dTE.Rows[0][2]))
            {
                try
                {
                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();
                    sqlConn.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    if (sqlConn.State == ConnectionState.Open)
                        sqlConn.Close();
                }
                UpdateDate();
            }
        }


        private void status_chouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlCommand sqlCmd;
            if (status_chouse.SelectedItem == "Прийнята")
                sqlCmd = new SqlCommand($"UPDATE [MY_DB].[dbo].[EventTable] SET [STATUS] = N'{status_chouse.SelectedItem}' , [DOC_NUM] = -1, [TERITORIA] = N'pls enter' WHERE [NUM] = {id}", sqlConn);
            else
                sqlCmd = new SqlCommand($"UPDATE [MY_DB].[dbo].[EventTable] SET [STATUS] = N'{status_chouse.SelectedItem}' , [DOC_NUM] = NULL, [TERITORIA] = NULL WHERE [NUM] = {id}", sqlConn);
            if (status_chouse.SelectedItem.ToString() != Convert.ToString(dTE.Rows[0][3]))
            {
                try
                {
                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();
                    sqlConn.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    if (sqlConn.State == ConnectionState.Open)
                        sqlConn.Close();
                }
                UpdateDate();
            }
        }

        private void numSpBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqlCommand sqlCmd = new SqlCommand($"UPDATE [MY_DB].[dbo].[EventTable] SET [DOC_NUM] = {numSpBox.Text} WHERE [NUM] = {id}", sqlConn);
            try
            {
                if (numSpBox.Text != Convert.ToString(dTE.Rows[0][4]))
                {
                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();
                    sqlConn.Close();
                    UpdateDate();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
        }

        private void adress_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqlCommand sqlCmd = new SqlCommand($"UPDATE [MY_DB].[dbo].[EventTable] SET [TERITORIA] = N'{adress_box.Text}' WHERE [NUM] = {id}", sqlConn);
            try
            {

                if (adress_box.Text != Convert.ToString(dTE.Rows[0][5]) && adress_box.Text != "" && adress_box.Text != "TextBox")
                {
                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();
                    sqlConn.Close();
                    UpdateDate();
                } 
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
        }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var row_list = GetDataGridRows(PeopleTable);
            foreach (DataGridRow single_row in row_list.Where(x => x.IsSelected))
            {
                try
                {
                    var num = dTP.Rows[single_row.GetIndex()].Field<int>("ID");
                    var com = $"DELETE FROM [SuspicionTable] WHERE [PERSON] = {num} AND [NUM] = {id}";
                    sqlConn.Open();
                    var cmd = new SqlCommand(com, sqlConn);
                    cmd.ExecuteNonQuery();
                    sqlConn.Close();
                    UpdateDate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var com = $"INSERT INTO [MY_DB].[dbo].[SuspicionTable] ([NUM] , [PERSON], [SUS]) VALUES ({id}, N'{add_people_chouse.Text.Split(' ')[0]}', N'{sus_chouse.Text}')";
            try
            {
                
                sqlConn.Open();
                var cmd = new SqlCommand(com, sqlConn);
                cmd.ExecuteNonQuery();
                sqlConn.Close();
                UpdateDate();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void PeopleTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row_list = GetDataGridRows((DataGrid)sender);
            foreach (DataGridRow single_row in row_list.Where(x => x.IsSelected))
            {
                try
                {
                    (new PersonWindow(dTP.Rows[single_row.GetIndex()].Field<int>("ID"))).Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //(new PersonWindow(-1)).Show();
                    //MessageBox.Show(ex.Message);
                }
            }
        }
    }
}