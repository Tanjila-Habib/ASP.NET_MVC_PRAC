using FormValidation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormValidation.Controllers
{
    public class FormHandlingController : Controller
    {
        // GET: FormHandling
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Register r)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index","Home");
            }
            return View(r);
        }
        
    }
}