using AutoMapper;
using ProjectManagement.auth;
using ProjectManagement.DTOs;
using ProjectManagement.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        PMS_Fall25_CEntities db = new PMS_Fall25_CEntities();
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDTO>().ReverseMap();
            });
            return new Mapper(config);
        }
        [Logged]
        public ActionResult Customer()
        {
            return View();
        }
        [Logged]
        public ActionResult MakeOrder()
        {
            var products = db.Products.ToList();
            //var data = GetMapper().Map<List<ProductDTO>>(products);
            //return View(data);
            return View(GetMapper().Map<List<ProductDTO>>(products));
        }
       
    }
}