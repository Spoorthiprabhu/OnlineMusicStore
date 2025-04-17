namespace OnlineMusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpotifyUrlToMusicItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MusicItems", "SpotifyUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MusicItems", "SpotifyUrl");
        }
    }
}
