namespace OnlineMusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameFilePathToSongUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MusicItems", "SongURL", c => c.String());
            DropColumn("dbo.MusicItems", "FilePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MusicItems", "FilePath", c => c.String());
            DropColumn("dbo.MusicItems", "SongURL");
        }
    }
}
