using Npgsql;
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
using NpgsqlTypes;

namespace Mentoring
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Database database = new Database();
            database.Connect("host=127.0.0.1;Port=5432;Database=Mentoring;User Id=postgres;Password=1234;");

            MainFrame.Navigate(PageControl.Authorization);
        }
    }
}
