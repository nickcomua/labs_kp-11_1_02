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
    /// Interaction logic for PersonWindow.xaml
    /// </summary>
    public partial class PersonWindow : Window
    {
        
        static int id = -1;
        static DataTable dTP = new DataTable("Людина");
        static DataTable dTEA = new DataTable("Всі події");
        static DataTable dTE = new DataTable("ПодіЇ участі");
        static SqlConnection sqlConn = new SqlConnection("Data Source=DESKTOP-52QTNQ9;Initial Catalog=MY_DB; Integrated Security=true;");
        void update_date()
        {
            sqlConn.Open();
            var comand = new SqlCommand($"SELECT * FROM [MY_DB].[dbo].[PeopleTable] WHERE [ID] = {id}", sqlConn);
            var adapter = new SqlDataAdapter(comand);
            dTP.Clear();
            adapter.Fill(dTP);
            if (dTP.Rows.Count > 0)
            {
                numBox.Text = dTP.Rows[0]["ID"].ToString();
                nameB.Text = dTP.Rows[0]["NAME"].ToString();
                surnameB.Text = dTP.Rows[0]["SURNAME"].ToString();
                midnameB.Text = dTP.Rows[0]["MIDNAME"].ToString();
                adress_box.Text = dTP.Rows[0]["ADRESS"].ToString();
                numSudBox.Text = dTP.Rows[0]["NUM_CON"].ToString();
            }
            comand = new SqlCommand($"SELECT * FROM [MY_DB].[dbo].[EventTable]", sqlConn);
            adapter = new SqlDataAdapter(comand);
            dTEA.Clear();
            adapter.Fill(dTEA);
            add_people_chouse.Items.Clear();
            foreach (DataRow i in dTEA.Rows)
                add_people_chouse.Items.Add($"{i[0]} {i.Field<DateTime>("DATE"):MM-dd-yyyy} {i[2]} | {i[3]}");
            comand = new SqlCommand($"SELECT [EventTable].* ,[SuspicionTable].[SUS] FROM[MY_DB].[dbo].[EventTable] INNER JOIN[MY_DB].[dbo].[SuspicionTable] ON[EventTable].[NUM] =[SuspicionTable].[NUM] WHERE [SuspicionTable].[PERSON] = {id}", sqlConn);
            adapter = new SqlDataAdapter(comand);
            dTE.Clear();
            adapter.Fill(dTE);
            EventTable.ItemsSource = dTE.DefaultView;
            sqlConn.Close();
        }

        public PersonWindow(int n)
        {           
            InitializeComponent();
            sus_chouse.Items.Add("Винуватець");
            sus_chouse.Items.Add("Підозрюваний");
            sus_chouse.Items.Add("Потерпілий");
            sus_chouse.Items.Add("Свідок");
            if (n == -1)
            {
                id = newperson();
            }
            else
            {
                id = n; 
            }
            update_date();
        }

        private int newperson()
        {
            sqlConn.Open();
            var comand = new SqlCommand($"SELECT MAX([ID]) FROM [MY_DB].[dbo].[PeopleTable]", sqlConn);
            var reader = comand.ExecuteReader();
            reader.Read();
            var idd = 1+reader.GetInt32(0);
            reader.Close();
            comand = new SqlCommand($"INSERT INTO [MY_DB].[dbo].[PeopleTable] VALUES(@ID,@NAME,@SURNAME,@MIDNAME,@ADRESS,@NUM_CON)", sqlConn);
            comand.Parameters.AddWithValue("@ID", idd);
            comand.Parameters.AddWithValue("@NAME", "");
            comand.Parameters.AddWithValue("@SURNAME", "");
            comand.Parameters.AddWithValue("@MIDNAME", "");
            comand.Parameters.AddWithValue("@ADRESS", "");
            comand.Parameters.AddWithValue("@NUM_CON", 0);
            comand.ExecuteNonQuery();            
            sqlConn.Close();
            return idd;
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[0-9]"); 
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void adress_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (adress_box.Text != "" && adress_box.Text != "TextBox" && adress_box.Text != dTP.Rows[0]["ADRESS"].ToString())
            {
                sqlConn.Open();
                var comand = new SqlCommand($"UPDATE [MY_DB].[dbo].[PeopleTable] SET [ADRESS] = N'{adress_box.Text}' WHERE [ID] = {id}", sqlConn);
                comand.ExecuteNonQuery();
                sqlConn.Close();
            } 
        }        

        private void numSpBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (numSudBox.Text != "" && numSudBox.Text != "TextBox" && numSudBox.Text != dTP.Rows[0]["NUM_CON"].ToString())
            {
                sqlConn.Open();
                var comand = new SqlCommand($"UPDATE [MY_DB].[dbo].[PeopleTable] SET [NUM_CON] = {numSudBox.Text} WHERE [ID] = {id}", sqlConn);
                comand.ExecuteNonQuery();
                sqlConn.Close();
            } 
        }

        private void surnameB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (surnameB.Text != "" && surnameB.Text != "TextBox" && surnameB.Text != dTP.Rows[0]["SURNAME"].ToString())
            {
                sqlConn.Open();
                var comand = new SqlCommand($"UPDATE [MY_DB].[dbo].[PeopleTable] SET [SURNAME] = N'{surnameB.Text}' WHERE [ID] = {id}", sqlConn);
                comand.ExecuteNonQuery();
                sqlConn.Close();
            } 
        }

        private void nameB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nameB.Text != "" && nameB.Text != "TextBox" && nameB.Text != dTP.Rows[0]["NAME"].ToString())
            {
                sqlConn.Open();
                var comand = new SqlCommand($"UPDATE [MY_DB].[dbo].[PeopleTable] SET [NAME] = N'{nameB.Text}' WHERE [ID] = {id}", sqlConn);
                comand.ExecuteNonQuery();
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
            var row_list = GetDataGridRows(EventTable);
            foreach (DataGridRow single_row in row_list.Where(x => x.IsSelected))
            {
                try
                {
                    var num = dTE.Rows[single_row.GetIndex()].Field<int>("NUM");
                    var com = $"DELETE FROM [SuspicionTable] WHERE [PERSON] = {id} AND [NUM] = {num}";
                    sqlConn.Open();
                    var cmd = new SqlCommand(com, sqlConn);
                    cmd.ExecuteNonQuery();
                    sqlConn.Close();
                    update_date();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // add sus
            if (add_people_chouse.SelectedIndex != -1)
            {
                string num = add_people_chouse.SelectedItem.ToString().Split(' ')[0];
                string sus = sus_chouse.SelectedItem.ToString();
                sqlConn.Open();
                var comand = new SqlCommand($"INSERT INTO [MY_DB].[dbo].[SuspicionTable] VALUES ({num}, {id}, N'{sus}')", sqlConn);
                comand.ExecuteNonQuery();
                sqlConn.Close();
                update_date();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EventTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row_list = GetDataGridRows((DataGrid)sender);
            foreach (DataGridRow single_row in row_list.Where(x => x.IsSelected))
            {
                try
                {
                    (new EventViev(dTE.Rows[single_row.GetIndex()].Field<int>("NUM"))).Show();
                }
                catch (Exception ex)
                {
                    //(new EventViev(-1)).Show();
                    //MessageBox.Show(ex.Message);
                }
            }
        } 
    }
}
