using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Mentoring
{
    public class Database
    {
        private static NpgsqlConnection connection;

        public void Connect(string connectionString)
        {
            if (connection == null)
                connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }

        public static NpgsqlCommand Command(string sql)
        {
            return new NpgsqlCommand(sql, connection);
        }
    }
}
