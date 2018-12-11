using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using University.Models;

namespace University.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet("/students")]
        public ActionResult Index()
        {
            List<Student> allStudents = Student.GetAll();
            return View(allStudents);
        }

        [HttpGet("/students/{id}")]
        public ActionResult Show(int id)
        {
            Student foundStudent = Student.Find(id);
            List<Course> courses = foundStudent.GetCourses();
            Dictionary<string, object> myDic = new Dictionary<string, object> ();
            List<Course> allCourses = Course.GetAll();
            myDic.Add("student", foundStudent);
            myDic.Add("courses", courses);
            myDic.Add("allCourses", allCourses);
            return View(myDic);
        }

        [HttpGet("/students/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/students")]
        public ActionResult Create(string studentName, string enrollmentDate)
        {
            Student newStudent = new Student(studentName, enrollmentDate);
            newStudent.Save();
            List<Student> allStudents = Student.GetAll();
            return View("Index", allStudents);
        }

        [HttpPost("/students/{id}")]
        public ActionResult AddCourse(int id, int courseId)
        {
            Student foundStudent = Student.Find(id);
            Course foundCourse = Course.Find(courseId);
            foundStudent.AddCourse(foundCourse);
            return RedirectToAction("Show");
        }
        
    }
}
