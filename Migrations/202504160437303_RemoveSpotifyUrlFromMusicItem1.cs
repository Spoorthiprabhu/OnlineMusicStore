namespace OnlineMusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSpotifyUrlFromMusicItem1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MusicItems", "SpotifyUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MusicItems", "SpotifyUrl", c => c.String());
        }
    }
}
