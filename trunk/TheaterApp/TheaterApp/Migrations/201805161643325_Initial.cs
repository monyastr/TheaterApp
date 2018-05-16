namespace TheaterApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        MovieID = c.Int(nullable: false, identity: true),
                        MovieName = c.String(nullable: false),
                        Logo = c.Binary(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MovieID);
            
            CreateTable(
                "dbo.Rate",
                c => new
                    {
                        RateID = c.Int(nullable: false, identity: true),
                        TheaterID = c.Int(nullable: false),
                        MovieID = c.Int(nullable: false),
                        MovieName = c.String(nullable: false),
                        TheaterName = c.String(),
                        MovieRate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RateID)
                .ForeignKey("dbo.Movie", t => t.MovieID, cascadeDelete: true)
                .ForeignKey("dbo.Theater", t => t.TheaterID, cascadeDelete: true)
                .Index(t => t.TheaterID)
                .Index(t => t.MovieID);
            
            CreateTable(
                "dbo.Theater",
                c => new
                    {
                        TheaterID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.TheaterID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rate", "TheaterID", "dbo.Theater");
            DropForeignKey("dbo.Rate", "MovieID", "dbo.Movie");
            DropIndex("dbo.Rate", new[] { "MovieID" });
            DropIndex("dbo.Rate", new[] { "TheaterID" });
            DropTable("dbo.Theater");
            DropTable("dbo.Rate");
            DropTable("dbo.Movie");
        }
    }
}
