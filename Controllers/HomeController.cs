using System.Web.Mvc;

namespace OnlineMusicStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult UserHome()
        {
            if (Session["UserId"] == null || Session["Role"] == null || Session["Role"].ToString() != "User")
                return RedirectToAction("Login", "Account");

            ViewBag.Username = Session["Username"];
            return View();
        }

        public ActionResult AdminHome()
        {
            System.Diagnostics.Debug.WriteLine("Session UserId: " + Session["UserId"]);
            System.Diagnostics.Debug.WriteLine("Session Role: " + Session["Role"]);

            if (Session["UserId"] == null || Session["Role"] == null || Session["Role"].ToString() != "Admin")
                return RedirectToAction("Login", "Account");

            ViewBag.Username = Session["Username"];
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }



    }
}
