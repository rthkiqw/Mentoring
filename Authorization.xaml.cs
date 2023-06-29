using Npgsql;
using NpgsqlTypes;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mentoring
{
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var login = tbLogin.Text.Trim();
            var password = tbPassword.Password.Trim();
            if (login != null && password != null)
                if (Auth(login, password))
                    NavigationService.Navigate(PageControl.StudentsPage);
        }

        public bool Auth(string login, string password)
        {
            NpgsqlCommand command = Database.Command("select login,password,\"lastName\",\"firstName\", patronymic from mentor where login = @login and password = @password");
            command.Parameters.AddWithValue("@login", NpgsqlDbType.Varchar, login);
            command.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, password);
            NpgsqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Mentor mentor = new Mentor(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                User.Set(mentor);
                string currentUser = string.Format("{0} {1} {2}", mentor.LastName, mentor.FirstName, mentor.Patronymic);
                Application.Current.MainWindow.Title = currentUser;
                reader.Close();
                return true;
            }
            else
                reader.Close();
                return false;
        }

        private void tbLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if( e.Key == Key.Enter)
            {
                var login = tbLogin.Text.Trim();
                var password = tbPassword.Password.Trim();
                if (login != null && password != null)
                    if (Auth(login, password))
                        NavigationService.Navigate(PageControl.StudentsPage);
            }
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var login = tbLogin.Text.Trim();
                var password = tbPassword.Password.Trim();
                if (login != null && password != null)
                    if (Auth(login, password))
                        NavigationService.Navigate(PageControl.StudentsPage);
            }
        }
    }
}
