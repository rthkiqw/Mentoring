using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mentoring
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string EducationType { get; set; }
        public bool Invalid { get; set; }
        public string Address { get; set; }
        public string HealthGroup { get; set; }
        public string Mentor { get; set; }

        public Student(int iD, string firstName, string lastName, string patronymic, DateTime birthday, string email, string phone, string educationType, bool invalid, string address, string healthGroup, string mentor)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Birthday = birthday;
            Email = email;
            Phone = phone;
            EducationType = educationType;
            Invalid = invalid;
            Address = address;
            HealthGroup = healthGroup;
            Mentor = mentor;
        }
    }
}
