using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineMusicStore.Models;

namespace OnlineMusicStore.Controllers
{
    public class OrderController : Controller
    {
        private MusicStoreContext db = new MusicStoreContext();

        public ActionResult UserOrders()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int userId = Convert.ToInt32(Session["UserId"]);

            var orders = db.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems.Select(oi => oi.MusicItem))
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }
    }
}