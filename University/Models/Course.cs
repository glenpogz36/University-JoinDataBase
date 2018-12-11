using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using University;

namespace University.Models
{
    public class Course
    {
        private int _id;
        private string _name;
        private string _description;

        public Course(string name, string description, int id = 0)
        {
            _name = name;
            _description = description;
            _id = id;
        }

        public string GetName()
        {
            return _name;
        }
        
        public string GetDescription()
        {
            return _description;
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
            cmd.CommandText = @"INSERT INTO courses (name, description) VALUES (@name, @description);";

            MySqlParameter name = new MySqlParameter();
            MySqlParameter description = new MySqlParameter();
            name.ParameterName = "@name";
            description.ParameterName = "@description";
            name.Value = this._name;
            description.Value = this._description;
            cmd.Parameters.Add(name);
            cmd.Parameters.Add(description);
            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Course> GetAll()
        {
            List<Course> allcourses = new List<Course> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM courses;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string description = rdr.GetString(2);
                Course newcourse = new Course (name, description, id);
                allcourses.Add(newcourse);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allcourses;
        }
           public static Course Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM courses WHERE id = (@searchId);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int courseId = 0;
            string CourseName = "";
            string description = "";

            while(rdr.Read())
            {
                courseId = rdr.GetInt32(0);
                CourseName = rdr.GetString(1);
                description = rdr.GetString(2);
            }

            Course newCourses = new Course(CourseName, description, courseId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newCourses;
        }

        public void AddStudent(Student newStudent)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO courses_students (course_id, student_id) VALUES (@coursesId, @studentId);";

            MySqlParameter courses_id = new MySqlParameter();
            courses_id.ParameterName = "@coursesId";
            courses_id.Value = _id;
            cmd.Parameters.Add(courses_id);

            MySqlParameter student_id = new MySqlParameter();
            student_id.ParameterName = "@studentId";
            student_id.Value = newStudent.GetId();
            cmd.Parameters.Add(student_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Student> GetStudents()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT students.* FROM courses
            JOIN courses_students ON (courses.id = courses_students.course_id)
            JOIN students ON (courses_students.student_id = students.id)
            WHERE courses.id = @courseId;";
        MySqlParameter courseIdParameter = new MySqlParameter();
        courseIdParameter.ParameterName = "@courseId";
        courseIdParameter.Value = _id;
        cmd.Parameters.Add(courseIdParameter);
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        List<Student> students = new List<Student>{};
        while(rdr.Read())
        {
          int studentId = rdr.GetInt32(0);
          string studentName = rdr.GetString(1);
          string enrollmentDate = rdr.GetDateTime(2).ToString();
          Student newstudent = new Student(studentName, enrollmentDate, studentId);
          students.Add(newstudent);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return students;
    }

     public void DeleteStudent(int studentId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses_students WHERE student_id = @studentId;";

      MySqlParameter coursesParameter = new MySqlParameter();
      coursesParameter.ParameterName = "@studentId";
      coursesParameter.Value = studentId;
      cmd.Parameters.Add(coursesParameter);

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM course;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

    }
    }
}
