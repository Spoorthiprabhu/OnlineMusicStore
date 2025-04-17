namespace OnlineMusicStore.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using OnlineMusicStore.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineMusicStore.Models.MusicStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OnlineMusicStore.Models.MusicStoreContext context)
        {
            if (!context.MusicCategories.Any())
            {
                context.MusicCategories.AddRange(new List<MusicCategory>
        {
            new MusicCategory { CategoryId = 1, CategoryName = "Classical" },
            new MusicCategory { CategoryId = 2, CategoryName = "Western" },
            new MusicCategory { CategoryId = 3, CategoryName = "Rock" },
        });

                context.SaveChanges();
            }

            if (!context.MusicItems.Any())
            {
                context.MusicItems.AddRange(new List<MusicItem>
{
    new MusicItem {
        Title = "Moonlight Sonata",
        Artist = "Beethoven",
        Genre = "Classical",
        ReleaseDate = new DateTime(1801, 1, 1),
        Price = 9.99m,
        SongURL = "~/Content/Music/sample.mp3", // keep as placeholder
        ImagePath = "https://via.placeholder.com/150",
        CategoryId = 1
    },
    new MusicItem {
        Title = "Bohemian Rhapsody",
        Artist = "Queen",
        Genre = "Rock",
        ReleaseDate = new DateTime(1975, 10, 31),
        Price = 14.99m,
        SongURL = "~/Content/Music/sample.mp3",
        ImagePath = "https://upload.wikimedia.org/wikipedia/en/9/9f/Bohemian_Rhapsody.png",
        CategoryId = 3
    }
});


                context.SaveChanges();
            }
        }



    }
}
