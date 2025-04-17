namespace OnlineMusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MusicCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.MusicItems", "MusicCategory_CategoryId", c => c.Int());
            CreateIndex("dbo.MusicItems", "MusicCategory_CategoryId");
            AddForeignKey("dbo.MusicItems", "MusicCategory_CategoryId", "dbo.MusicCategories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MusicItems", "MusicCategory_CategoryId", "dbo.MusicCategories");
            DropIndex("dbo.MusicItems", new[] { "MusicCategory_CategoryId" });
            DropColumn("dbo.MusicItems", "MusicCategory_CategoryId");
            DropTable("dbo.MusicCategories");
        }
    }
}
