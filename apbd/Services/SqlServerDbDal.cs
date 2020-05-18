using System.Collections.Generic;
using apbd.Controllers;
using apbd.Models;

namespace apbd.Services
{
    public class SqlServerDbDal : IStudentsDal
    {
        private static IEnumerable<Student> _students;

        static SqlServerDbDal()
        {
            _students = new List<Student>();
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
