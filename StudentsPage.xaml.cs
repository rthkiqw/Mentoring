using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mentoring
{
    public partial class StudentsPage : Page
    {
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
        public ObservableCollection<string> HealthGroups { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> EducationTypes { get; set; } = new ObservableCollection<string>();
        public StudentsPage()
        {
            InitializeComponent();

            LoadHealthGroups();
            LoadEducationTypes();
            LoadStudents();
            cbInvalid.Items.Add("Все");
            cbInvalid.Items.Add("Нет");
            cbInvalid.Items.Add("Есть");
            cbInvalid.SelectedIndex = 0;
            cbHealthGroup.SelectedIndex = 0;
            cbEducationType.SelectedIndex = 0;
            cbEducationType.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
            {
                Source = EducationTypes
            });
            cbHealthGroup.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
            {
                Source = HealthGroups
            });
            lvStudents.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
            {
                Source = Students
            });
        }

        private void LoadStudents()
        {
            NpgsqlCommand command = Database.Command("select student.\"ID\" ,\r\n\"firstName\",\r\n\"lastName\" ,\r\n\"patronymic\",\r\n\"birthday\" ,\r\n\"email\" ,\r\n\"phone\",\r\neT.\"name\",\r\n\"invalid\",\r\n\"address\",\r\nhG.\"name\" ,\r\n\"mentor\" from student inner join \"educationType\" eT on eT.\"ID\" = student.\"educationType\" INNER JOIN \"healthGroup\" hG on hG.\"ID\" = student.\"healthGroup\" where mentor = @mentor ORDER BY \"lastName\"");
            command.Parameters.AddWithValue("@mentor", NpgsqlDbType.Varchar, User.Current().Login);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Students.Add(new Student(reader.GetInt32(0),
                    reader.GetString(1), reader.GetString(2),
                    reader.GetString(3), reader.GetDateTime(4),
                    reader.GetString(5), reader.GetString(6),
                    reader.GetString(7), reader.GetBoolean(8),
                    reader.GetString(9), reader.GetString(10),
                    reader.GetString(11)));
            }
            reader.Close();
        }

        private void LoadEducationTypes()
        {
            EducationTypes.Add("Все");
            NpgsqlCommand command = Database.Command("select name from \"educationType\"");
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                EducationTypes.Add(reader.GetString(0));
            }
            reader.Close();
        }

        private void LoadHealthGroups()
        {
            HealthGroups.Add("Все");
            NpgsqlCommand command = Database.Command("select name from \"healthGroup\"");
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HealthGroups.Add(reader.GetString(0));
            }
            reader.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddStudentPage addStudentPage = PageControl.AddStudentPage;
            addStudentPage.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addStudentPage.ShowDialog();
        }

        private void EducationTypeFilter(string edType)
        {
            var view = CollectionViewSource.GetDefaultView(lvStudents.ItemsSource);
            if (view == null)
                return;
            view.Filter = o =>
            {
                if (edType == "Все")
                    return true;
                Student student = o as Student;
                if (student.EducationType == edType)
                    return true;
                return false;
            };
        }

        private void HealthGroupFilter(string HealthGroup)
        {
            var view = CollectionViewSource.GetDefaultView(lvStudents.ItemsSource);
            if (view == null)
                return;
            view.Filter = o =>
            {
                if (HealthGroup == "Все")
                    return true;
                Student student = o as Student;
                if (student.HealthGroup == HealthGroup)
                    return true;
                return false;
            };
        }

        private void InvalidFilter(string invalid)
        {
            var view = CollectionViewSource.GetDefaultView(lvStudents.ItemsSource);
            if (view == null)
                return;
            view.Filter = o =>
            {
                if (invalid == "Все")
                    return true;
                Student student = o as Student;
                if (invalid == "Есть")
                    if (student.Invalid == true)
                        return true;
                if (invalid == "Нет")
                    if (student.Invalid == false)
                        return true;
                return false;
            };
        }

        private void cbEducationType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var edType = cbEducationType.SelectedItem.ToString();
            EducationTypeFilter(edType);
        }

        private void cbHealthGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var HealthGroup = cbHealthGroup.SelectedItem.ToString();
            HealthGroupFilter(HealthGroup);
        }

        private void cbInvalid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var invalid = cbInvalid.SelectedItem.ToString();
            InvalidFilter(invalid);
        }
    }
}
