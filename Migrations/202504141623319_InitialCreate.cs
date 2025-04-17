namespace OnlineMusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        CartItemId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MusicItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CartItemId)
                .ForeignKey("dbo.MusicItems", t => t.MusicItemId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MusicItemId);
            
            CreateTable(
                "dbo.MusicItems",
                c => new
                    {
                        MusicItemId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Artist = c.String(),
                        Genre = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FilePath = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.MusicItemId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Message = c.String(),
                        SubmittedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FeedbackId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        WishListId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MusicItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WishListId)
                .ForeignKey("dbo.MusicItems", t => t.MusicItemId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MusicItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishLists", "UserId", "dbo.Users");
            DropForeignKey("dbo.WishLists", "MusicItemId", "dbo.MusicItems");
            DropForeignKey("dbo.Feedbacks", "UserId", "dbo.Users");
            DropForeignKey("dbo.CartItems", "UserId", "dbo.Users");
            DropForeignKey("dbo.CartItems", "MusicItemId", "dbo.MusicItems");
            DropIndex("dbo.WishLists", new[] { "MusicItemId" });
            DropIndex("dbo.WishLists", new[] { "UserId" });
            DropIndex("dbo.Feedbacks", new[] { "UserId" });
            DropIndex("dbo.CartItems", new[] { "MusicItemId" });
            DropIndex("dbo.CartItems", new[] { "UserId" });
            DropTable("dbo.WishLists");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Users");
            DropTable("dbo.MusicItems");
            DropTable("dbo.CartItems");
        }
    }
}
