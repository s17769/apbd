using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using apbd.Models;
using apbd.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apbd.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        // https://www.connectionstrings.com/sql-server/
        private const string ConString = "Data Source=localhost;Initial Catalog=master;User ID=SA;Password=haslo123!@#";

        private IStudentsDal _dbService;

        public StudentsController(IStudentsDal dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents([FromServices] IStudentsDal dbService)
        {
            var list = new List<Student>();

            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT * FROM Student";

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = DateTime.Parse(dr["BirthDate"].ToString());
                    st.IdEnrollment = Int32.Parse(dr["IdEnrollment"].ToString());
                    list.Add(st);
                }

            }

            return Ok(list);
        }



        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(String indexNumber)
        {
            var list = new List<Student>();

            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT * FROM Student WHERE IndexNumber = @IndexNumber";
                com.Parameters.Add("@IndexNumber", SqlDbType.NVarChar);
                com.Parameters["@IndexNumber"].Value = indexNumber;

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = DateTime.Parse(dr["BirthDate"].ToString());
                    st.IdEnrollment = Int32.Parse(dr["IdEnrollment"].ToString());
                    list.Add(st);
                }

            }

            return Ok(list);
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {  
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                student.IndexNumber = $"s{new Random().Next(1, 20000)}";
                com.Connection = con;
                com.CommandText = "INSERT INTO Student VALUES " +
                    "(@IndexNumber, @FirstName, @LastName, @BirthDate, @IdEnrollment)";
                com.Parameters.Add("@IndexNumber", SqlDbType.NVarChar);
                com.Parameters["@IndexNumber"].Value = student.IndexNumber;
                com.Parameters.Add("@FirstName", SqlDbType.NVarChar);
                com.Parameters["@FirstName"].Value = student.FirstName;
                com.Parameters.Add("@LastName", SqlDbType.NVarChar);
                com.Parameters["@LastName"].Value = student.LastName;
                com.Parameters.Add("@BirthDate", SqlDbType.Date);
                com.Parameters["@BirthDate"].Value = student.BirthDate;
                com.Parameters.Add("@IdEnrollment", SqlDbType.Int);
                com.Parameters["@IdEnrollment"].Value = student.IdEnrollment;

                con.Open();
                try
                {
                    int rowsAffected = com.ExecuteNonQuery();
                } catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return BadRequest();
                }
            }

            return Ok(student);
        }

        [HttpPut("{indexNumber}")]
        public IActionResult UpdateStudent(String indexNumber, Student student)
        {
            return Ok("Aktualizacja zakończona");
        }

        [HttpDelete("{id}")]
            public IActionResult DeleteStudent(int id)
        {
            //FIXME delete a student with matching id
            return Ok("Usuwanie ukończone");
        }

    }
}
