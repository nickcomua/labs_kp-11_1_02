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
    /// Interaction logic for UserForm.xaml
    /// </summary>
    public partial class UserForm : Window
    {
        static SqlConnection sqlConn = new SqlConnection("Data Source=DESKTOP-52QTNQ9;Initial Catalog=MY_DB;" + "Integrated Security=true;");//давай
        static SqlCommand Com;
        static SqlDataAdapter Data;
        static DataTable dT = new DataTable();
        static DataGrid dataGrid = new DataGrid();
        static int LenTable;
        static int count = 0;
        static string login, passwd;
        public UserForm()
        {
            InitializeComponent();
        } 

        private void close_butt_Click(object sender, RoutedEventArgs e)
        {
            Close();
            (new MainWindow()).Show();
        }

        private void register_butt_Click(object sender, RoutedEventArgs e)
        { 
            sqlConn.Open();
            String nameReg = NameField.Text;
            String surnameReg = SurnameField.Text;
            String loginReg = loginRegField.Text;
            String passwdReg = passwdRegField.Password;
            String passwdReg2 = passwdRegField2.Password;
            String strQ;
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    if ((passwdReg == passwdReg2) && (loginReg != "") && (passwdReg != ""))
                    {
                        strQ = "INSERT INTO MainTable ";
                        strQ += "VALUES ('" + nameReg + "', '" + surnameReg + "', '" + loginReg + "', '" + passwdReg + "', 'True', 'False'); "; 
                    Com = new SqlCommand(strQ, sqlConn);
                        Com.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("Паролі не однакові або логін/паролі не введений"); 
                    }
                }
                catch
                {
                    MessageBox.Show("Користувач з таким логіном вже існує у системі!"); 
                }
            }
            sqlConn.Close();

        }

        private bool RestrictionFunc(string Pass)
        { 
            Byte Count1, Count2, Count3;
            Byte LenPass = (Byte)Pass.Length;
            Count1 = Count2 = Count3 = 0;
            for (Byte i = 0; i < LenPass; i++)
            {
                if ((Convert.ToInt32(Pass[i]) >= 65) &&
               (Convert.ToInt32(Pass[i]) <= 65 + 25))
                    Count1++;
                if ((Convert.ToInt32(Pass[i]) >= 97) &&
               (Convert.ToInt32(Pass[i]) <= 97 + 25))
                    Count2++;
                if ((Pass[i] == '+') || (Pass[i] == '-') || (Pass[i] == '*') || (Pass[i] == '/'))
                    Count3++;
            }
            return (Count1 * Count2 * Count3 != 0);

        }

        private void logout_but_Click(object sender, RoutedEventArgs e)
        {
            NewNameField.Text = ""; NewNameField.IsEnabled = false;
            NewSurnameField.Text = ""; NewSurnameField.IsEnabled = false; NewPasswdField.Password = ""; 
            NewPasswdField.IsEnabled = false; NewPasswdField2.Password = ""; NewPasswdField2.IsEnabled = false; 
            UpdateDataBtn.IsEnabled = false;
            passwdField.Password = "";
        }

        private void UpdateDataBtn_Click(object sender, RoutedEventArgs e)
        { 
            sqlConn.Open();
            String newname = NewNameField.Text;
            String newsurname = NewSurnameField.Text;
            String newpasswd = NewPasswdField.Password;
            String newpasswd2 = NewPasswdField2.Password;
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                String strQ;
                if ((newpasswd == newpasswd2) && (newpasswd != ""))
                { 
                    if (Convert.ToBoolean(dT.Rows[0][5]) == true)
                    {
                        if (RestrictionFunc(newpasswd))
                        {
                            strQ = "UPDATE MainTable SET Name='" + newname + "', "; strQ += "Surname='" + newsurname + "', "; strQ += "Password='" + newpasswd + "' ";
                            strQ += "WHERE Login='" + login + "';";
                            //MessageBox.Show(strQ); 
                            Com = new SqlCommand(strQ, sqlConn);
                            Com.ExecuteNonQuery();
                        }
                        else
                            MessageBox.Show("У паролі немає літер верхнього та нижнього регістрів, а також арифметичних операцій! Спробуйте знову!"); 
                    }
                    else
                    {
                        strQ = "UPDATE MainTable SET Name='" + newname + "', "; strQ += "Surname='" + newsurname + "', ";
                        strQ += "Password='" + newpasswd + "' ";
                        strQ += "WHERE Login='" + login + "';";
                        Com = new SqlCommand(strQ, sqlConn);
                        Com.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("Введено пустий пароль або новий пароль повторно введено некоректно!");
                }
            }
            sqlConn.Close();

        }

        private void auth_butt_Click(object sender, RoutedEventArgs e)
        {
            login = loginField.Text;
            passwd = passwdField.Password; 
            sqlConn.Open();
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                String strQ = "SELECT * FROM MainTable WHERE Login='" + login + "';"; Data = new SqlDataAdapter(strQ, sqlConn);
                dT = new DataTable("Користувачі системи");
                Data.Fill(dT);
                if (dT.Rows.Count == 0)
                    MessageBox.Show("Такого користувача на знайдено");
                else
                {
                    bool Status = Convert.ToBoolean(dT.Rows[0][4]);
                    if (!Status)
                        MessageBox.Show("Користувач заблокований Адміністратором системи!"); 
                    else
                    {
                        if ((dT.Rows[0][2].ToString() == login) &&
                       (dT.Rows[0][3].ToString() == passwd))
                        {
                            NewNameField.Text = dT.Rows[0][0].ToString();
                            NewSurnameField.Text = dT.Rows[0][1].ToString(); NewPasswdField.Password = "";
                            NewPasswdField2.Password = "";
                            NewNameField.IsEnabled = true;
                            NewSurnameField.IsEnabled = true;
                            NewPasswdField.IsEnabled = true;
                            NewPasswdField2.IsEnabled = true;
                            UpdateDataBtn.IsEnabled = true;
                        }
                        else
                        {
                            count++;
                            String s = "Невірно введений пароль! " +
                           "Помилкове введення №" + count.ToString();
                            MessageBox.Show(s);
                            if (count == 3)
                                System.Windows.Application.Current.Shutdown();
                        }
                    }
                }
            }
            sqlConn.Close();


        }
    }
}
