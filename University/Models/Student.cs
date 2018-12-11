using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using University;

namespace University.Models
{
    public class Student
    {
        private int _id;
        private string _name;
        private string _enrollmentDate;

        public Student(string name, string enrollmentDate, int id = 0)
        {
            _name = name;
            _enrollmentDate = enrollmentDate;
            _id = id;
        }

        public string GetName()
        {
            return _name;
        }
        
        public string GetEnrollmentDate()
        {
            return _enrollmentDate;
        }

        public int GetId()
        {
            return _id;
        }


        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO students (name, enrollmentDate) VALUES (@name, @enrollmentDate);";

            MySqlParameter name = new MySqlParameter();
            MySqlParameter enrollmentDate = new MySqlParameter();
            name.ParameterName = "@name";
            enrollmentDate.ParameterName = "@enrollmentDate";
            name.Value = this._name;
            enrollmentDate.Value = this._enrollmentDate;
            cmd.Parameters.Add(name);
            cmd.Parameters.Add(enrollmentDate);
            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Student> GetAll()
        {
            List<Student> allStudents = new List<Student> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM students;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string enrollmentDate = rdr.GetDateTime(2).ToString();
                Student newStudent = new Student(name, enrollmentDate, id);
                allStudents.Add(newStudent);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStudents;
        }

        public static Student Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM students WHERE id = (@searchId);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int StudentId = 0;
            string StudentName = "";
            string StudentEnrollmentDate = "";

            while(rdr.Read())
            {
                StudentId = rdr.GetInt32(0);
                StudentName = rdr.GetString(1);
                StudentEnrollmentDate = rdr.GetDateTime(2).ToString();
            }

            Student newStudent = new Student(StudentName, StudentEnrollmentDate, StudentId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newStudent;
        }

         public void AddCourse(Course newCourse)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO courses_students (course_id, student_id) VALUES (@coursesId, @studentId);";

            MySqlParameter courses_id = new MySqlParameter();
            courses_id.ParameterName = "@studentId";
            courses_id.Value = _id;
            cmd.Parameters.Add(courses_id);

            MySqlParameter course_id = new MySqlParameter();
            course_id.ParameterName = "@coursesId";
            course_id.Value = newCourse.GetId();
            cmd.Parameters.Add(course_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Course> GetCourses()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT courses.* FROM students
                JOIN courses_students ON (students.id = courses_students.student_id)
                JOIN courses ON (courses_students.course_id = courses.id)
                WHERE students.id = @studentId;";
            MySqlParameter studentIdParameter = new MySqlParameter();
            studentIdParameter.ParameterName = "@studentId";
            studentIdParameter.Value = _id;
            cmd.Parameters.Add(studentIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Course> Courses = new List<Course>{};
            while(rdr.Read())
            {
            int CourseId = rdr.GetInt32(0);
            string CourseName = rdr.GetString(1);
            string description = rdr.GetString(2);
            Course newCourse = new Course(CourseName, description, CourseId);
            Courses.Add(newCourse);
            }
            conn.Close();
            if (conn != null)
            {
            conn.Dispose();
            }
            return Courses;
            }
    }
}
