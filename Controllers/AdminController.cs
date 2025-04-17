using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineMusicStore.Models;

namespace OnlineMusicStore.Controllers
{
    public class AdminController : Controller
    {
        private MusicStoreContext db = new MusicStoreContext();
        public ActionResult ManageUsers()
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
                return RedirectToAction("Login", "Account");

            var users = db.Users.ToList(); 
            return View(users);
        }

        public ActionResult DeleteUser(int id)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
                return RedirectToAction("Login", "Account");

            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();

            // Remove related votes to avoid foreign key conflict
            var votes = db.Votes.Where(v => v.UserId == id).ToList();
            db.Votes.RemoveRange(votes);

            db.Users.Remove(user);
            db.SaveChanges();

            TempData["Success"] = "User deleted successfully.";
            return RedirectToAction("ManageUsers");
        }


        public ActionResult ViewFeedback()
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
                return RedirectToAction("Login", "Account");

            var feedbacks = db.Feedbacks.Include("User").OrderByDescending(f => f.SubmittedAt).ToList();
            return View(feedbacks);
        }

        public ActionResult SalesReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SalesReport(DateTime? selectedDate)
        {
            var orders = db.Orders
                           .Include("OrderItems")
                           .Include("User")
                           .AsQueryable();

            DateTime today = DateTime.Today;
            ViewBag.ReportType = "All";

            if (selectedDate.HasValue)
            {
                
                DateTime startDate = selectedDate.Value.Date;
                DateTime endDate = startDate.AddDays(1);

                orders = orders.Where(o => o.OrderDate >= startDate && o.OrderDate < endDate);
                ViewBag.ReportType = $"Sales for {selectedDate.Value.ToShortDateString()}";
            }
            else if (Request.Form["reportType"] == "week")
            {
                var lastWeek = today.AddDays(-7);
                orders = orders.Where(o => o.OrderDate >= lastWeek && o.OrderDate <= today);
                ViewBag.ReportType = "Sales - Last 7 Days";
            }
            else if (Request.Form["reportType"] == "month")
            {
                var lastMonth = today.AddMonths(-1);
                orders = orders.Where(o => o.OrderDate >= lastMonth && o.OrderDate <= today);
                ViewBag.ReportType = "Sales - Last 30 Days";
            }

            return View(orders.ToList());
        }


        // GET: /Admin/EditSong/5
        public ActionResult EditSong(int id)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
                return RedirectToAction("Login", "Account");

            var song = db.MusicItems.Find(id);
            if (song == null)
                return HttpNotFound();
           ViewBag.Categories = db.MusicCategories.ToList();

            return View(song);
        }

        // POST: /Admin/EditSong/5
        [HttpPost]
        public ActionResult EditSong(MusicItem updatedItem)
        {
            if (ModelState.IsValid)
            {
                var existingItem = db.MusicItems.Find(updatedItem.MusicItemId);
                if (existingItem != null)
                {
                    existingItem.Title = updatedItem.Title;
                    existingItem.Artist = updatedItem.Artist;
                    existingItem.ReleaseDate = updatedItem.ReleaseDate;
                    existingItem.Price = updatedItem.Price;
                    existingItem.SongURL = updatedItem.SongURL;
                    existingItem.CategoryId = updatedItem.CategoryId;

                    db.SaveChanges();

                    // Redirect to category page after update
                    var category = db.MusicCategories.Find(existingItem.CategoryId);
                    

                    return RedirectToAction("BrowseByCategory", "Music", new { id = existingItem.CategoryId });

                }
            }

            // If not valid or not found, return to edit view
            ViewBag.Categories = new SelectList(db.MusicCategories.ToList(), "CategoryId", "CategoryName", updatedItem.CategoryId);
            return View(updatedItem);
        }

        
          

            public ActionResult AllOrders()
            {
                var orders = db.Orders
                    .Include(o => o.OrderItems.Select(oi => oi.MusicItem))
                    .Include(o => o.User) // if you want customer info
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();

                return View(orders);
            }
        








    }
}