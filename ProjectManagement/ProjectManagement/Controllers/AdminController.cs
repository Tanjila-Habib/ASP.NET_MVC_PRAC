using ProjectManagement.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagement.Controllers
{
    public class AdminController : Controller
    {   PMS_Fall25_CEntities dbo=new PMS_Fall25_CEntities();
        // GET: Admin
        public ActionResult Dashboard()
        {   List<Order>orders = new List<Order>();  
            foreach(var item in dbo.Orders)
            {
                orders.Add(item);
            }
            return View(orders);
        }
        public ActionResult Accept(int id)
        {
            Order order = dbo.Orders.Find(id);

            if (order != null)
            {
                if (order.Status == "Ordered")
                {
                    order.Status = "Processing";
                    dbo.SaveChanges();
                }
            }

            return RedirectToAction("Dashboard");
        }
        public ActionResult Decline(int id)
        {
            Order order = dbo.Orders.Find(id);

            if (order != null)
            {
                if (order.Status == "Ordered")
                {
                    // return product quantities
                    foreach (var item in dbo.OrderDetails)
                    {
                        if (item.OId == order.Id)
                        {
                            var product = dbo.Products.Find(item.PId);
                            product.Qty += item.Qty;
                        }
                    }

                    order.Status = "Declined";
                    dbo.SaveChanges();
                }
            }

            return RedirectToAction("Dashboard");
        }

    }
}
   
    



