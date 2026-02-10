
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationPractice.EF;
using WebApplicationPractice.Models;





namespace WebApplicationPractice.Controllers
{
    public class StudentsSchoolController : Controller
    {
         StudentDBEntities dbo = new StudentDBEntities();
        // GET: Students
        public ActionResult Index()
        {
            var data = dbo.SchoolStudents.ToList();
            return View(data);

           
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View(new Student ());
        }

        [HttpPost]
        public ActionResult Register(Student s)

        {
            // Custom: Email must match ID
            if (s.Email != s.StudentId + "@student.aiub.edu")
            {
                ModelState.AddModelError("Email", "Email must match Student ID (XX-XXXXX-X@student.aiub.edu).");
            }

            // Model validation check
            if (!ModelState.IsValid)
            {
                return View(s);
            }
            var entity = new SchoolStudent
            {
                FullName = s.FullName,
                StudentId = s.StudentId,
                Email = s.Email,
                Age = s.Age,
                EnrollmentDate = s.EnrollmentDate
            };
            // Save to DB
            dbo.SchoolStudents.Add(entity);
            dbo.SaveChanges();

            TempData["Msg"] = "Student Registered!";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var data=dbo.SchoolStudents.Find(id);
            return View(data);
        }
        [HttpGet]
        public ActionResult Edit(int id) {
            var data = dbo.SchoolStudents.Find(id);
            return View(data);
        }
        [HttpPost]
       
        public ActionResult Edit(SchoolStudent s)
        {
            var obj = dbo.SchoolStudents.Find(s.Id);

            if (obj == null)
            {
                return HttpNotFound();
            }

            obj.FullName = s.FullName;
            obj.StudentId = s.StudentId;
            obj.Email = s.Email;
           

            dbo.SaveChanges();

            TempData["Msg"] = "Data Updated";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var stu = dbo.SchoolStudents.Find(id);
            dbo.SchoolStudents.Remove(stu);
            dbo.SaveChanges();

            TempData["msg"] = "student deleted successfully";
            return RedirectToAction("index");
        }

        public ActionResult Search(string id)
        {
            var data = (from s in dbo.SchoolStudents
                        where s.FullName.Contains(id)
                        select s).ToList();
            return View(data);
        }

    }
}