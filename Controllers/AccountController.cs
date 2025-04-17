using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineMusicStore.Models;

namespace OnlineMusicStore.Controllers
{
    

    public class AccountController : Controller
    {
        private MusicStoreContext db = new MusicStoreContext();

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(User user)
        {
            var loginUser = db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (loginUser != null)
            {
                Session["UserId"] = loginUser.UserId;
                Session["Username"] = loginUser.Username;
                Session["Role"] = loginUser.Role;

                if (loginUser.Role == "Admin")
                    return RedirectToAction("AdminHome", "Home");
                else
                    return RedirectToAction("UserHome", "Home");
            }

            ViewBag.Message = "Invalid username or password.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        


    }

}