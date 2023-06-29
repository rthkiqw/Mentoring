using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring
{
    public class PageControl
    {
        private static Authorization _authorization;
        public static Authorization Authorization
        {
            get
            {
                if (_authorization == null)
                    _authorization = new Authorization();
                return _authorization;
            }
        }

        private static StudentsPage _studentsPage;
        public static StudentsPage StudentsPage
        {
            get
            {
                if (_studentsPage == null)
                    _studentsPage = new StudentsPage();
                return _studentsPage;
            }
        }

        private static AddStudentPage _addStudentPage;
        public static AddStudentPage AddStudentPage
        {
            get
            {
                if (_addStudentPage == null)
                    _addStudentPage = new AddStudentPage();
                return _addStudentPage;
            }
            set
            {
                _addStudentPage = value;
            }
        }
    }
}
