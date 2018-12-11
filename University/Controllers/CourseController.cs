using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using University.Models;

namespace University.Controllers
{
    public class CourseController : Controller
    {
        [HttpGet("/courses")]
        public ActionResult Index()
        {
            List<Course> allourses = Course.GetAll();
            return View(allourses);
        }

        [HttpGet("/courses/{id}")]
        public ActionResult Show(int id)
        {
            Course foundCourse = Course.Find(id);
            List<Student> students = foundCourse.GetStudents();
            List<Student> allStudents = Student.GetAll();
            Dictionary<string, object> myDic = new Dictionary<string, object> ();
            myDic.Add("course", foundCourse);
            myDic.Add("students", students);
            myDic.Add("allStudents", allStudents);
            return View(myDic);
        }

        [HttpGet("/courses/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/courses")]
        public ActionResult Create(string courseName, string description)
        {
            Course newCourse = new Course(courseName, description);
            newCourse.Save();
            List<Course> allcourses = Course.GetAll();
            return View("Index", allcourses);
        }

        [HttpPost("/courses/{id}")]
        public ActionResult AddStudent(int id, int studentId)
        {
            Course foundCourse = Course.Find(id);
            Student foundStudent = Student.Find(studentId);
            foundCourse.AddStudent(foundStudent);
            return RedirectToAction("Show");
        }

        [HttpPost("/courses/{id}/delete")]
        public ActionResult DeleteStudent(int id, int studentId)
        {
            Course foundCourse = Course.Find(id);
            foundCourse.DeleteStudent(studentId);
            return RedirectToAction("Show");
        }
    }
}
