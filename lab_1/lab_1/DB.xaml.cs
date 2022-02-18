using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
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

namespace lab_1
{
    /// <summary>
    /// Логика взаимодействия для DB.xaml
    /// </summary>

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
    public partial class DB : Window
    {
        public DB()
        {
            InitializeComponent();
        }

        private void ToHome(object sender, RoutedEventArgs e)
        {
            var mw = new MainWindow();
            mw.Show();
            Hide(); 

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            string pathCsvFile = "DB.csv";

            
            var student = new Student();
            student.ID = int.Parse(id1.Text);
            student.Name = name.Text;
            student.Comment = comment.Text;
            File.AppendAllText(pathCsvFile, String.Format("{0};{1};{2}\n", student.ID, student.Name, student.Comment));
        }

        private void Del(object sender, RoutedEventArgs e)
        {
            var students = new List<Student>();
            var student = new Student();
            string pathCsvFile = "DB.csv";
            using (StreamReader streamReader = new StreamReader(pathCsvFile))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    student = new Student();
                    var vals = line.Split(';');
                    student.ID = Convert.ToInt32(vals[0]);
                    student.Name = vals[1];
                    student.Comment = vals[2];
                    students.Add(student);
                }
            }

            students = students.FindAll(x => x.ID != int.Parse(id2.Text)).ToList();

            var csv = new StringBuilder();

            foreach (var stud in students) 
            {
                var newLine = String.Format("{0};{1};{2}", stud.ID, stud.Name, stud.Comment);
                csv.AppendLine(newLine);
            }
            File.WriteAllText(pathCsvFile, csv.ToString());
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            string pathCsvFile = "DB.csv";
            ((TextBox)sender).Text = File.ReadAllText(pathCsvFile);

        }
    }
}
