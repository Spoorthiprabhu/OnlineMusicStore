namespace OnlineMusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryToMusicItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MusicItems", "MusicCategory_CategoryId", "dbo.MusicCategories");
            DropIndex("dbo.MusicItems", new[] { "MusicCategory_CategoryId" });
            RenameColumn(table: "dbo.MusicItems", name: "MusicCategory_CategoryId", newName: "CategoryId");
            AlterColumn("dbo.MusicItems", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.MusicItems", "CategoryId");
            AddForeignKey("dbo.MusicItems", "CategoryId", "dbo.MusicCategories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MusicItems", "CategoryId", "dbo.MusicCategories");
            DropIndex("dbo.MusicItems", new[] { "CategoryId" });
            AlterColumn("dbo.MusicItems", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.MusicItems", name: "CategoryId", newName: "MusicCategory_CategoryId");
            CreateIndex("dbo.MusicItems", "MusicCategory_CategoryId");
            AddForeignKey("dbo.MusicItems", "MusicCategory_CategoryId", "dbo.MusicCategories", "CategoryId");
        }
    }
}
