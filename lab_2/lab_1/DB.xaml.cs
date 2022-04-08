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
    /*
     <Grid>
        <Grid.RowDefinitions>
            <RowDefinition></RowDefinition>
            <RowDefinition></RowDefinition>
            <RowDefinition></RowDefinition>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition></ColumnDefinition>
            <ColumnDefinition></ColumnDefinition>
            <ColumnDefinition></ColumnDefinition>
            <ColumnDefinition></ColumnDefinition>
        </Grid.ColumnDefinitions>

        <Button Content="To Home" HorizontalAlignment="Center" VerticalAlignment="Center" Width="94" Height="46" Grid.Column="3" Grid.Row="2" Click="ToHome"/>
        <TextBox x:Name="id1" HorizontalAlignment="Center" Margin="0,100,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="120"/>
        <TextBox x:Name="comment" HorizontalAlignment="Center" Grid.Row="1" TextWrapping="Wrap" VerticalAlignment="Top" Width="400" Height="75" Grid.ColumnSpan="2" Margin="0,75,0,0"/>
        <TextBox x:Name="name" Grid.Column="1" HorizontalAlignment="Center" Margin="0,100,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="267" Grid.ColumnSpan="2"/>
        <Label Content="ID" HorizontalAlignment="Center" VerticalAlignment="Top" FontSize="36" Margin="0,31,0,0"/>
        <Label Grid.Column="1" Content="Full Name" HorizontalAlignment="Center" Margin="0,30,0,0" VerticalAlignment="Top" FontSize="36" Grid.ColumnSpan="2"/>
        <Label Content="Comment" HorizontalAlignment="Center" Margin="0,10,0,0" VerticalAlignment="Top" FontSize="36" Grid.ColumnSpan="2" Grid.Row="1" Width="174"/>
        <Button Content="Add" HorizontalAlignment="Center" VerticalAlignment="Center" Width="94" Height="46" Grid.Column="2" Grid.Row="1" Click="Add"/>
        <Button Content="Del" HorizontalAlignment="Center" VerticalAlignment="Center" Width="94" Height="46" Grid.Column="2" Grid.Row="2" Click="Del"/>
        <TextBox x:Name="id2" HorizontalAlignment="Left" Margin="140,100,0,0" TextWrapping="Wrap" Text="TextBox" VerticalAlignment="Top" Width="120" Grid.ColumnSpan="2" Grid.Row="2"/>
        <Label Content="ID" HorizontalAlignment="Left" VerticalAlignment="Top" FontSize="36" Margin="178,31,0,0" Grid.ColumnSpan="2" Grid.Row="2"/>
        <TextBox Grid.Column="3" HorizontalAlignment="Center" TextWrapping="Wrap" Text="TextBox" VerticalAlignment="Top" Width="200" Height="300" Grid.RowSpan="2" GotFocus="TextBox_GotFocus"/>
    </Grid>
     */
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
    public partial class DB : Window
    {
        TextBox id1 = new TextBox();
        TextBox id2 = new TextBox();
        TextBox name = new TextBox();
        TextBox comment = new TextBox();
        public DB()
        {
            InitializeComponent();
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            Button button_tohome = new Button();
            button_tohome.Content = "To Home";
            button_tohome.HorizontalAlignment = HorizontalAlignment.Center;
            button_tohome.VerticalAlignment = VerticalAlignment.Center;
            button_tohome.Width = 94;
            button_tohome.Height = 46;
            button_tohome.Click += ToHome;
            Grid.SetColumn(button_tohome, 3);
            Grid.SetRow(button_tohome, 2);
            grid.Children.Add(button_tohome);

            
            id1.HorizontalAlignment = HorizontalAlignment.Center;
            id1.VerticalAlignment = VerticalAlignment.Top;
            id1.Margin = new Thickness(0,100,0,0);
            id1.TextWrapping = TextWrapping.Wrap;
            id1.Width = 120;
            grid.Children.Add(id1);

            
            id2.HorizontalAlignment = HorizontalAlignment.Left;
            id2.VerticalAlignment = VerticalAlignment.Top;
            id2.Margin = new Thickness(140, 100, 0, 0);
            id2.TextWrapping = TextWrapping.Wrap;
            id2.Width = 120;
            Grid.SetColumnSpan(id2, 2);
            Grid.SetRow(id2, 2);
            grid.Children.Add(id2);

            comment.HorizontalAlignment = HorizontalAlignment.Center;
            comment.VerticalAlignment = VerticalAlignment.Top;
            comment.Margin = new Thickness(0, 75, 0, 0);
            comment.TextWrapping = TextWrapping.Wrap;
            comment.Width = 400;
            comment.Height = 75;
            Grid.SetColumnSpan(comment, 2);
            Grid.SetRow(comment, 1);
            grid.Children.Add(comment);

            
            name.HorizontalAlignment = HorizontalAlignment.Center;
            name.VerticalAlignment = VerticalAlignment.Top;
            name.Margin = new Thickness(0, 100, 0, 0);
            name.TextWrapping = TextWrapping.Wrap;
            name.Width = 267;
            Grid.SetColumn(name, 1);
            Grid.SetColumnSpan(name, 2);
            grid.Children.Add(name);

            Label label1 = new Label();
            label1.HorizontalAlignment = HorizontalAlignment.Center;
            label1.VerticalAlignment = VerticalAlignment.Top;
            label1.Content = "ID";
            label1.FontSize = 36;
            label1.Margin = new Thickness(0,31,0,0);
            grid.Children.Add(label1);

            Label label2 = new Label();
            label2.Margin = new Thickness(0,30,0,0);
            label2.Content = "Full Name";
            label2.HorizontalAlignment = HorizontalAlignment.Center;
            label2.VerticalAlignment = VerticalAlignment.Top;
            label2.FontSize = 36;
            Grid.SetColumnSpan(label2, 2);
            Grid.SetColumn(label2, 1);
            grid.Children.Add(label2);

            Label label3 = new Label();
            label3.Content = "Comment";
            label3.HorizontalAlignment = HorizontalAlignment.Center;
            label3.VerticalAlignment = VerticalAlignment.Top;
            label3.Margin = new Thickness(0,10,0,0);
            label3.FontSize = 36;
            label3.Width = 174;
            Grid.SetColumnSpan(label3 , 2);
            Grid.SetRow(label3 , 1);   
            grid.Children.Add(label3);

            Label label4 = new Label();
            label4.Content = "ID";
            label4.HorizontalAlignment = HorizontalAlignment.Left;
            label4.VerticalAlignment = VerticalAlignment.Top;
            label4.FontSize = 36;
            label4.Margin = new Thickness(178,31,0,0);
            Grid.SetColumnSpan(label4 , 2);
            Grid.SetRow(label4 , 2);
            grid.Children.Add(label4);

            TextBox hzbaton = new TextBox();
            hzbaton.HorizontalAlignment = HorizontalAlignment.Center;
            hzbaton.VerticalAlignment = VerticalAlignment.Top;
            hzbaton.TextWrapping = TextWrapping.Wrap;
            hzbaton.Text = "TB";
            hzbaton.Width = 200;
            hzbaton.Height = 300;
            hzbaton.GotFocus += TextBox_GotFocus;
            Grid.SetColumn(hzbaton , 3);
            Grid.SetRowSpan(hzbaton , 2);
            grid.Children.Add(hzbaton);

            Button butadd = new Button();
            butadd.Content = "Add";
            butadd.HorizontalAlignment = HorizontalAlignment.Center;
            butadd.VerticalAlignment = VerticalAlignment.Center;
            butadd.Width = 94;
            butadd.Height = 46;
            Grid.SetRow(butadd , 1);
            Grid.SetColumn(butadd , 2);
            butadd.Click += Add;
            grid.Children.Add(butadd);

            Button butdel = new Button();
            butdel.Content = "Del";
            butdel.HorizontalAlignment = HorizontalAlignment.Center;
            butdel.VerticalAlignment = VerticalAlignment.Center;
            butdel.Width = 94;
            butdel.Height = 46;
            Grid.SetRow(butdel, 2);
            Grid.SetColumn(butdel, 2);
            butdel.Click += Del;
            grid.Children.Add(butdel);

            Dwin.Content = grid;
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
