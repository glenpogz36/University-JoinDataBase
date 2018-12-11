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
            Course foundCourses = Course.Find(id);
            return View(foundCourses);
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
    }
}
