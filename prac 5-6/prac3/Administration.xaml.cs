using System;
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

namespace prac3
{
    /// <summary>
    /// Interaction logic for Administration.xaml
    /// </summary>
    public partial class Administration : Window
    {
        static SqlConnection sqlConn = new SqlConnection("Data Source=DESKTOP-52QTNQ9;Initial Catalog=MY_DB;" + "Integrated Security=true;");//давай
        static SqlCommand Com;
        static SqlDataAdapter Data;
        static DataTable dT = new DataTable();
        static DataGrid dataGrid = new DataGrid();
        static int LenTable;
        static int index = 0;
        public Administration()
        { 
            InitializeComponent();
        }

        void UpdateDataTable()
        { 
            sqlConn.Open();
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                Data = new SqlDataAdapter("SELECT Name, Surname, Login, Status, Restriction FROM MainTable", sqlConn);
                dT = new DataTable("Користувачі системи");
                Data.Fill(dT);
                dataGrid.ItemsSource = dT.DefaultView;
                LenTable = dT.Rows.Count;
                UsersLogins.ItemsSource = dT.DefaultView;
                //add logins to combobox
                comboboxx.Items.Clear();
                for (int i = 0; i < LenTable; i++)
                {
                    comboboxx.Items.Add(dT.Rows[i][2].ToString());
                } 
            }
            sqlConn.Close();
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            (new MainWindow()).Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
              
            sqlConn.Open();
            String strQ;
            String RealPass = RealAdminPasswd.Password.ToString(); String NewPass = NewAdminPasswd.Password.ToString(); String NewPass2 = NewAdminPasswd2.Password.ToString(); 
            if ((RealPass == AdminPasswd.Password.ToString()) && (NewPass != "") && (NewPass == NewPass2))
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    strQ = "UPDATE MainTable SET Password ='" + NewPass + "' WHERE Login='ADMIN';";
                    Com = new SqlCommand(strQ, sqlConn);
                    Com.ExecuteNonQuery();
                }
            }
            sqlConn.Close();
        }

        private void prev_butt_Click(object sender, RoutedEventArgs e)
        {
            if (index > 0)
            {
                index--;
                name_l.Content = dT.Rows[index][0].ToString(); 
                surname_l.Content = dT.Rows[index][1].ToString(); 
                login_l.Content = dT.Rows[index][2].ToString(); 
                status_l.Content = dT.Rows[index][3].ToString(); 
                pswd_l.Content = dT.Rows[index][4].ToString();
            }
        }

        private void next_butt_Click(object sender, RoutedEventArgs e)
        {
            if (index < LenTable - 1)
            {
                index++;
                name_l.Content = dT.Rows[index][0].ToString();
                surname_l.Content = dT.Rows[index][1].ToString();
                login_l.Content = dT.Rows[index][2].ToString();
                status_l.Content = dT.Rows[index][3].ToString();
                pswd_l.Content = dT.Rows[index][4].ToString();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        { 
            
            String strQ;
            String UserLogin = add_login.Text;
            try
            {
                sqlConn.Open();
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    strQ = "INSERT INTO MainTable (Name, Surname, Login, Status, Restriction) values('', '', '" + UserLogin + "', 1, 0);";
                    Com = new SqlCommand(strQ, sqlConn);
                    Com.ExecuteNonQuery();
                }
                sqlConn.Close();
                UpdateDataTable();
            }
            catch
            {
                MessageBox.Show("Користувача не додано! Можливо такий в базі вже є!");
            }
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            String strQ;
            bool UserStatus = (bool)ChangeActive.IsChecked;
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                strQ = "UPDATE MainTable SET Status ='" + UserStatus + "' WHERE Login='" + comboboxx.SelectedValue.ToString() + "';";
                Com = new SqlCommand(strQ, sqlConn);
                Com.ExecuteNonQuery();
            }
            sqlConn.Close();
            UpdateDataTable();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        { 
            sqlConn.Open();
            String strQ;
            bool UserRestriction = (bool)ChangeRestriction.IsChecked; if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                strQ = "UPDATE MainTable SET Restriction ='" + UserRestriction + "' WHERE Login='" + comboboxx.SelectedValue.ToString() + "';";
                Com = new SqlCommand(strQ, sqlConn);
                Com.ExecuteNonQuery();
            }
            sqlConn.Close();
            UpdateDataTable();

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            RealAdminPasswd.Password = ""; RealAdminPasswd.IsEnabled = false; 
            NewAdminPasswd.Password = ""; NewAdminPasswd.IsEnabled = false; 
            NewAdminPasswd2.Password = ""; NewAdminPasswd2.IsEnabled = false; 
            prev_butt.IsEnabled = false; next_butt.IsEnabled = false; UpdatePasswd.IsEnabled = false;
            AddUser.IsEnabled = false;
            CorrectStatusBtn.IsEnabled = false;
            CorrectRestrictionBtn.IsEnabled = false;
            dT.Clear();
            dataGrid.ItemsSource = dT.DefaultView;
            AdminPasswd.Password = "";
            UsersLogins.ItemsSource = "";
        }

        private void login_butt_Click(object sender, RoutedEventArgs e)
        {
            //find in dT the row with login = ADMIN and get the password
            sqlConn.Open();
            String strQ;
            bool flag = false;
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                strQ = "SELECT Password FROM MainTable WHERE Login='ADMIN';";
                Com = new SqlCommand(strQ, sqlConn);
                SqlDataReader reader = Com.ExecuteReader();
                while (reader.Read())
                {
                    flag = RealAdminPasswd.Password == reader[0].ToString();
                }
                reader.Close();
            }
            sqlConn.Close();
            
            if (flag)
            {
                UpdateDataTable();
                //enable all objects
                RealAdminPasswd.IsEnabled = true;
                NewAdminPasswd.IsEnabled = true;
                NewAdminPasswd2.IsEnabled = true;
                prev_butt.IsEnabled = true;
                next_butt.IsEnabled = true;
                UpdatePasswd.IsEnabled = true;
                AddUser.IsEnabled = true;
                CorrectStatusBtn.IsEnabled = true;
                CorrectRestrictionBtn.IsEnabled = true;
                ChangeActive.IsEnabled = true;
                ChangeRestriction.IsEnabled = true;
                UsersLogins.IsEnabled = true;
                AdminPasswd.IsEnabled = true;
                comboboxx.IsEnabled = true;
                Logout_butt.IsEnabled = true;                
            }
            else
            {
                MessageBox.Show("Невірний пароль!");
            }   
        }
    }
}
