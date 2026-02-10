using AutoMapper;
using ProjectManagement.DTOs;
using ProjectManagement.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace ProjectManagement.Controllers
{
    public class RegistrationController : Controller
    {
       PMS_Fall25_CEntities dbo=new PMS_Fall25_CEntities();

        // GET: Registration
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CustomerDTO, Customer>().ReverseMap();

            });
            return new Mapper(config);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new CustomerDTO());
        }
        [HttpPost]
        public ActionResult Index(CustomerDTO c)
        {
            if (ModelState.IsValid)
            {
                var customer = GetMapper().Map<Customer>(c);
                customer.Password=CreateMD5(customer.Password);
                customer.Role = "Customer";

                dbo.Customers.Add(customer);
                dbo.SaveChanges();
                TempData["Msg"] = "Registration Successful";
                return RedirectToAction("Login");
            }
            return View(c);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
       
        public ActionResult Login(LoginDTO log)
        {
            log.Password = CreateMD5(log.Password);

            var user = (from c in dbo.Customers
                        where c.Username.Equals(log.Username)
                        && c.Password.Equals(log.Password)
                        select c).FirstOrDefault();

            if (user != null)
            {
                if (user.Role == "Admin")
                {
                    Session["Admin"] = user;
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    Session["Logged"] = user;
                    return RedirectToAction("Customer", "Dashboard");
                }
            }

            TempData["Msg"] = "Username Password Invalid";
            return RedirectToAction("Login");
        }

        public static string CreateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // "x2" ensures lowercase hex format
                }
                return sb.ToString();
            }
        }
        
    }
}