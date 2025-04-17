namespace OnlineMusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVoteTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteId = c.Int(nullable: false, identity: true),
                        MusicItemId = c.Int(nullable: false),
                        UserId = c.Int(),
                        VotedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.MusicItems", t => t.MusicItemId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.MusicItemId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Votes", "MusicItemId", "dbo.MusicItems");
            DropIndex("dbo.Votes", new[] { "UserId" });
            DropIndex("dbo.Votes", new[] { "MusicItemId" });
            DropTable("dbo.Votes");
        }
    }
}
