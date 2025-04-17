using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnlineMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);
        //}
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            using (var context = new Models.MusicStoreContext())
            {
                if (!context.MusicCategories.Any())
                {
                    var classical = new Models.MusicCategory { CategoryName = "Classical" };
                    var rock = new Models.MusicCategory { CategoryName = "Rock" };

                    context.MusicCategories.Add(classical);
                    context.MusicCategories.Add(rock);
                    context.SaveChanges();

                    context.MusicItems.Add(new Models.MusicItem
                    {
                        Title = "Moonlight Sonata",
                        Artist = "Beethoven",
                        Genre = "Classical",
                        ReleaseDate = new DateTime(1801, 1, 1),
                        Price = 9.99m,
                        CategoryId = classical.CategoryId,
                        SongURL = "~/Content/Music/moonlight.mp3",
                        ImagePath = "~/Content/Images/moonlight.jpg"
                    });

                    context.MusicItems.Add(new Models.MusicItem
                    {
                        Title = "Bohemian Rhapsody",
                        Artist = "Queen",
                        Genre = "Rock",
                        ReleaseDate = new DateTime(1975, 1, 1),
                        Price = 14.99m,
                        CategoryId = rock.CategoryId,
                        SongURL = "~/Content/Music/bohemian.mp3",
                        ImagePath = "~/Content/Images/bohemian.jpg"
                    });

                    context.SaveChanges();
                }
            }
        }

    }
}
