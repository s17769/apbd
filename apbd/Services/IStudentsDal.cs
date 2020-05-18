using System.Collections.Generic;
using apbd.Models;

namespace apbd.Services
{
    public interface IStudentsDal
    {
        public IEnumerable<Student> GetStudents();
    }
}
