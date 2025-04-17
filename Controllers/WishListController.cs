using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineMusicStore.Models;

namespace OnlineMusicStore.Controllers
{
    public class WishListController : Controller
    {
        private MusicStoreContext db = new MusicStoreContext();

        // Add to wishlist
        public ActionResult AddToWishList(int id)
        {

            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int userId = Convert.ToInt32(Session["UserId"]);

            var existing = db.WishLists.FirstOrDefault(w => w.UserId == userId && w.MusicItemId == id);
            if (existing == null)
            {
                var wish = new WishList
                {
                    UserId = userId,
                    MusicItemId = id
                };
                db.WishLists.Add(wish);
                db.SaveChanges();
                TempData["Success"] = "Added to wishlist!";
            }
            else
            {
                TempData["Info"] = "This item is already in your wishlist.";
            }

            return RedirectToAction("Categories", "Music");
        }

        

        // Remove from wishlist
        public ActionResult RemoveFromWishList(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int userId = Convert.ToInt32(Session["UserId"]);
            var item = db.WishLists.FirstOrDefault(w => w.UserId == userId && w.MusicItemId == id);

            if (item != null)
            {
                db.WishLists.Remove(item);
                db.SaveChanges();
                TempData["Success"] = "Removed from wishlist.";
            }

            return RedirectToAction("ViewWishList");
        }


        [HttpGet]
        public ActionResult MoveToCart(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int userId = Convert.ToInt32(Session["UserId"]);

            // Check if item is already in cart
            var existingCartItem = db.CartItems
                .FirstOrDefault(c => c.UserId == userId && c.MusicItemId == id);

            if (existingCartItem == null)
            {
                // Add to cart
                CartItem newItem = new CartItem
                {
                    UserId = userId,
                    MusicItemId = id,
                    Quantity = 1,
                    AddedDate = DateTime.Now
                };
                db.CartItems.Add(newItem);
            }

            // Remove from wishlist
            var wishItem = db.WishLists
                .FirstOrDefault(w => w.UserId == userId && w.MusicItemId == id);

            if (wishItem != null)
            {
                db.WishLists.Remove(wishItem);
            }

            db.SaveChanges();

            TempData["Success"] = "Item moved to cart successfully!";
            return RedirectToAction("ViewWishList");
        }


        // View wishlist
        public ActionResult ViewWishList()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int userId = Convert.ToInt32(Session["UserId"]);
            var wishListItems = db.WishLists
                .Where(w => w.UserId == userId)
                .Select(w => w.MusicItem)
                .ToList();

            return View(wishListItems);
        }



    }
}