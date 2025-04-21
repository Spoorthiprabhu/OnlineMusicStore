using OnlineMusicStore.Models; // Namespace for your models
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class MusicController : Controller
{
    private MusicStoreContext db = new MusicStoreContext();

    // GET: Music/BrowseByCategory/1
    public ActionResult BrowseByCategory(int id)
    {
        var category = db.MusicCategories
            .Include("MusicItems")
            .FirstOrDefault(c => c.CategoryId == id);

        if (category == null)
        {
            return HttpNotFound();
        }

        ViewBag.CategoryName = category.CategoryName;
        return View(category.MusicItems.ToList()); // This now returns a real List<MusicItem>


    }


    public ActionResult Categories()
    {
        var categories = db.MusicCategories.ToList();
        return View(categories);
    }

    [HttpGet]
    public ActionResult AddMusic()
    {
        if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
        {
            return RedirectToAction("Login", "Account");
        }

        ViewBag.CategoryId = new SelectList(db.MusicCategories, "CategoryId", "CategoryName");
        return View();
    }

    [HttpPost]
    public ActionResult AddMusic(MusicItem item, HttpPostedFileBase ImageFile, HttpPostedFileBase MusicFile)
    {
        if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
        {
            return RedirectToAction("Login", "Account");
        }

        if (ImageFile != null)
        {
            string imagePath = "~/Content/Images/" + ImageFile.FileName;
            ImageFile.SaveAs(Server.MapPath(imagePath));
            item.ImagePath = imagePath;
        }

        //if (MusicFile != null)
        //{
        //    string musicPath = "~/Content/Music/" + MusicFile.FileName;
        //    MusicFile.SaveAs(Server.MapPath(musicPath));
        //    item.SongURL = musicPath;
        //}

        db.MusicItems.Add(item);
        db.SaveChanges();

        TempData["Success"] = "Music item added successfully!";
        return RedirectToAction("AddMusic");
    }

    public ActionResult Search(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return View(new List<MusicItem>());
        }

        var results = db.MusicItems
            .Where(m => m.Title.Contains(query) || m.Artist.Contains(query) || m.Genre.Contains(query))
            .ToList();

        ViewBag.SearchQuery = query;
        return View(results);
    }

    public ActionResult AddToCart(int id)
    {
        if (Session["UserId"] == null)
        {
            return RedirectToAction("Login", "Account");
        }

        int userId = Convert.ToInt32(Session["UserId"]);

        var music = db.MusicItems.Find(id);
        if (music == null)
            return HttpNotFound();

        var existingItem = db.CartItems.FirstOrDefault(c => c.UserId == userId && c.MusicItemId == id);
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            var cartItem = new CartItem
            {
                UserId = userId,
                MusicItemId = id,
                Quantity = 1,
                AddedDate = DateTime.Now
            };
            db.CartItems.Add(cartItem);
        }

        db.SaveChanges();

        TempData["Success"] = $"{music.Title} added to your cart!";
        return RedirectToAction("Categories");
    }


    public ActionResult Cart()
    {
        if (Session["UserId"] == null)
            return RedirectToAction("Login", "Account");

        int userId = Convert.ToInt32(Session["UserId"]);

        var cartItems = db.CartItems
                          .Where(c => c.UserId == userId)
                          .Include("MusicItem")
                          .ToList();

        return View(cartItems);
    }


    public ActionResult RemoveFromCart(int id)
    {
        var item = db.CartItems.Find(id);
        if (item == null)
        {
            return HttpNotFound();
        }

        db.CartItems.Remove(item);
        db.SaveChanges();

        TempData["Success"] = "Item removed from cart!";
        return RedirectToAction("Cart");
    }

    public ActionResult BuyNow(int id)
    {
        try
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int userId = Convert.ToInt32(Session["UserId"]);

            var cartItem = db.CartItems
                             .FirstOrDefault(c => c.CartItemId == id && c.UserId == userId);

            if (cartItem == null)
                return Content("⚠️ cartItem is null. ID passed: " + id + ", userId: " + userId);

            if (cartItem.MusicItemId == 0)
                return Content("⚠️ cartItem.MusicItemId is 0 or invalid.");

            var musicItem = db.MusicItems.Find(cartItem.MusicItemId);

            if (musicItem == null)
                return Content("⚠️ MusicItem with ID " + cartItem.MusicItemId + " not found in DB.");

            cartItem.MusicItem = musicItem;

            var orderItem = new OrderItem
            {
                MusicItemId = cartItem.MusicItemId,
                Quantity = cartItem.Quantity,
                Price = musicItem.Price
            };

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = musicItem.Price * cartItem.Quantity,
                OrderItems = new List<OrderItem> { orderItem }
            };

            db.Orders.Add(order);
            db.CartItems.Remove(cartItem);
            db.SaveChanges(); // ✅ Order saved, ID generated here

            TempData["Success"] = $"🎉 You purchased '{musicItem.Title}' successfully!";

            // ✅ Redirect with the new Order ID
            return RedirectToAction("OrderConfirmation", new { id = order.OrderId });
        }
        catch (Exception ex)
        {
            return Content("❌ Exception caught: " + ex.Message + "<br><br>" + ex.StackTrace);
        }
    }



    public ActionResult OrderConfirmation(int id)
    {
        var order = db.Orders
                      .Include(o => o.OrderItems.Select(oi => oi.MusicItem))
                      .Include(o => o.User)
                      .FirstOrDefault(o => o.OrderId == id);

        if (order == null)
            return HttpNotFound("Order not found.");

        return View(order);
    }


    public ActionResult Vote()
    {
        if (Session["UserId"] == null)
            return RedirectToAction("Login", "Account");

        int userId = Convert.ToInt32(Session["UserId"]);
        var musicList = db.MusicItems.ToList();
        return View(musicList);
    }

    [HttpPost]
    public ActionResult Vote(int musicItemId)
    {
       
        var vote = new Vote
        {
            MusicItemId = musicItemId,
            VotedAt = DateTime.Now,
            UserId = Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : (int?)null
        };

        db.Votes.Add(vote);
        db.SaveChanges();

        TempData["Success"] = "Your vote has been recorded!";
        return RedirectToAction("Vote");
    }



public ActionResult ChartToppers()
{
    var topSongs = db.Votes
        .GroupBy(v => v.MusicItemId)
        .Select(group => new
        {
            MusicItemId = group.Key,
            VoteCount = group.Count()
        })
        .OrderByDescending(x => x.VoteCount)
        .Take(10)
        .ToList();

    var musicItems = topSongs.Select(x => new ChartTopperViewModel
    {
        Music = db.MusicItems.Find(x.MusicItemId),
        Votes = x.VoteCount
    }).ToList();

    return View(musicItems);
}


    [HttpPost]
    public ActionResult SubmitVote(int musicItemId)
    {
        if (Session["UserId"] == null)
            return RedirectToAction("Login", "Account");

        int userId = Convert.ToInt32(Session["UserId"]);

        // Allow multiple votes — no duplicate check
        Vote vote = new Vote
        {
            MusicItemId = musicItemId,
            UserId = userId,
            VotedAt = DateTime.Now
        };

        db.Votes.Add(vote);
        db.SaveChanges();

        TempData["Success"] = "Thank you for your vote!";
        return RedirectToAction("Vote");
    }






}
