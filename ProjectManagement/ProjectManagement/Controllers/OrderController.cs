using PMS.Controllers;
using ProjectManagement.auth;
using ProjectManagement.DTOs;
using ProjectManagement.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagement.Controllers
{
    [Logged]
    public class OrderController : Controller
    {
        PMS_Fall25_CEntities dbo = new PMS_Fall25_CEntities();

        // GET: Order
        public ActionResult AddtoCart(int id)
        {
            var pd = dbo.Products.Find(id);
            var mapper = DashboardController.GetMapper();
            var p = mapper.Map<ProductDTO>(pd);
            List<ProductDTO> cart = null;
            if (Session["Cart"] == null)
            {
                cart = new List<ProductDTO>();
            }
            else
            {
                cart = (List<ProductDTO>)Session["Cart"];
            }
            p.Qty = 1;
            cart.Add(p);
            Session["Cart"] = cart;
            return RedirectToAction("MakeOrder", "Dashboard");

        }
        public ActionResult Increase(int id)
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowCart");
            }

            var cart = (List<ProductDTO>)Session["Cart"];
            ProductDTO found = null;

            foreach (var item in cart)
            {
                if (item.Id == id)
                {
                    found = item;
                    break;
                }
            }

            if (found != null)
            {
                var product = dbo.Products.Find(id);
                if (product.Qty > 0)
                {
                    found.Qty++;
                    product.Qty--;
                    dbo.SaveChanges();
                }
            }

            Session["Cart"] = cart;
            return RedirectToAction("ShowCart");
        }
        public ActionResult Decrease(int id)
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowCart");
            }

            var cart = (List<ProductDTO>)Session["Cart"];
            ProductDTO found = null;//create a variable to store product from cart

            foreach (var item in cart)
            {
                if (item.Id == id)//check if the current item is the one we want
                {
                    found = item;//store the matched product and save the product in the found
                    break;//stops the loop
                }
            }

            if (found != null)//if the product is not found in the cart
            {
                var product = dbo.Products.Find(id);
                found.Qty--;//user used (-) button,decreased by 1
                product.Qty++;//one item is removed from the cart,item goes back to stock

                if (found.Qty == 0)
                {
                    cart.Remove(found);
                }

                dbo.SaveChanges();
            }

            Session["Cart"] = cart;
            return RedirectToAction("ShowCart");
        }

        public ActionResult ShowCart()
        {
            var cart = Session["Cart"];
            if (cart == null)
            {
                return RedirectToAction("MakeOrder", "Dashboard");
            }
            else
            {
                var data = (List<ProductDTO>)Session["Cart"];
                return View(data);
            }
        }
        public ActionResult RemoveFromCart(int id)
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("MakeOrder", "Dashboard");
            }

            var cart = (List<ProductDTO>)Session["Cart"];
            ProductDTO found = null;

            foreach (var item in cart)
            {
                if (item.Id == id)
                {
                    found = item;
                    break;
                }
            }

            if (found != null)
            {
                
                var product = dbo.Products.Find(id);
                product.Qty += found.Qty;

                cart.Remove(found);
                dbo.SaveChanges();
            }

            Session["Cart"] = cart;
            return RedirectToAction("ShowCart");
        }

        [HttpPost]
        public ActionResult Place(decimal Total)
        {
            var customer = (Customer)Session["Logged"];

            var order = new Order()
            {
                Amount = Total,
                Status = "Ordered",
                CusId = customer.Id,
                Date = DateTime.Now
            };
            dbo.Orders.Add(order);
            dbo.SaveChanges();
            var cart = (List<ProductDTO>)Session["Cart"];
            foreach (var item in cart)
            {
                var od = new OrderDetail()
                {
                    PId = item.Id,
                    OId = order.Id,
                    Price = item.Price,
                    Qty = item.Qty
                };
                dbo.OrderDetails.Add(od);
            }
            dbo.SaveChanges();
            Session["Cart"] = null;
            TempData["Msg"] = "Order Placed";
            return RedirectToAction("Customer", "Dashboard");
        }
        public ActionResult MyOrders()
        {
            var customer = (Customer)Session["Logged"];

            List<Order> orders = new List<Order>();

            foreach (var item in dbo.Orders)
            {
                if (item.CusId == customer.Id)
                {
                    orders.Add(item);
                }
            }

            return View(orders);
        }
        public ActionResult CancelOrder(int id)
        {
            var customer = (Customer)Session["Logged"];

            // Find order
            Order order = dbo.Orders.Find(id);

            if (order == null)
            {
                return RedirectToAction("Customer", "Dashboard");
            }

            // Security check: only own order
            if (order.CusId != customer.Id)
            {
                return RedirectToAction("Customer", "Dashboard");
            }

            // Allow cancel ONLY if status is Ordered
            if (order.Status != "Ordered")
            {
                TempData["Msg"] = "Order cannot be cancelled";
                return RedirectToAction("Customer", "Dashboard");
            }

            // Get order details
            var details = dbo.OrderDetails.Where(d => d.OId == id).ToList();

            foreach (var d in details)
            {
                var product = dbo.Products.Find(d.PId);
                product.Qty += d.Qty;   // return stock
            }

            // Update order status
            order.Status = "Cancelled";

            dbo.SaveChanges();

            TempData["Msg"] = "Order cancelled successfully";
            return RedirectToAction("Customer", "Dashboard");
        }


    }

}
