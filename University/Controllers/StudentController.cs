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
            return View(foundStudent);
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
    }
}
