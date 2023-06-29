using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring
{
    public static class User
    {
        private static Mentor _mentor;

        public static void Set(Mentor mentor)
        {
            _mentor = mentor;
        }

        public static Mentor Current()
        {
            if (_mentor == null)
                return null;
            return _mentor;
        }
    }
}
