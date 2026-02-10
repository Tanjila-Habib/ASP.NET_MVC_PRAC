using Formhandling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Formhandling.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View(new Login() { });
        }
        [HttpPost]
        public ActionResult Index(Login login)
        {  if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(login);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Student s)
        {  if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(s);
        }
    }
}