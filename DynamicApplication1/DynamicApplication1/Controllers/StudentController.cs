
using DynamicApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicApplication1.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            //ViewBag.S1 = "Rahim";
            //ViewBag.S2 = "Karim";
            //ViewBag.S3 = "Sattar";

            var s1 = new Student()
            {
                ID= 1,
                Name = "Rahim",
                CGPA = 2.34f
            };
            var s2 = new Student()
            {
                ID = 2,
                Name = "Karim",
                CGPA = 2.34f
            };
            var s3 = new Student()
            {
                ID = 3,
                Name="Sattar",
                CGPA=3.90f
            };
            var students=new[] { s1, s2, s3 };

            return View(students);
        }
      
        public ActionResult Create()
        {
            return View();
        }
    }
}