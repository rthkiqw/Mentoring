using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring
{
    public class Mentor
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }

        public Mentor(string login, string password, string lastName, string firstName, string patronymic)
        {
            Login = login;
            Password = password;
            LastName = lastName;
            FirstName = firstName;
            Patronymic = patronymic;
        }
    }
}
