using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineMusicStore.Models;


namespace OnlineMusicStore.Controllers
{
    public class FeedbackController : Controller
    {
        private MusicStoreContext db = new MusicStoreContext();

        // GET: Feedback/Submit
        [HttpGet]
        public ActionResult Submit()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: Feedback/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(Feedback feedback)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                feedback.UserId = Convert.ToInt32(Session["UserId"]);
                feedback.SubmittedAt = DateTime.Now;

                db.Feedbacks.Add(feedback);
                db.SaveChanges();

                TempData["Success"] = "Thank you for your feedback!";
                return RedirectToAction("UserHome", "Home");
            }

            return View(feedback);
        }

        // Optional: Admin View
        public ActionResult List()
        {
            if (Session["Role"]?.ToString() != "Admin")
                return RedirectToAction("Login", "Account");

            var feedbacks = db.Feedbacks.Include("User").OrderByDescending(f => f.SubmittedAt).ToList();
            return View(feedbacks);
        }
    }
}
