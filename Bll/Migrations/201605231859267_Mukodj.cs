namespace Bll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mukodj : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Guess",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.Int(nullable: false),
            //            MatchId = c.Int(nullable: false),
            //            HomeScore = c.Int(nullable: false),
            //            AwayScore = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Match", t => t.MatchId)
            //    .ForeignKey("dbo.User", t => t.UserId)
            //    .Index(t => t.UserId)
            //    .Index(t => t.MatchId);
            
            //CreateTable(
            //    "dbo.Match",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            TournamentId = c.Int(nullable: false),
            //            HomeTeamId = c.Int(nullable: false),
            //            AwayTeamId = c.Int(nullable: false),
            //            Date = c.DateTime(nullable: false),
            //            Type = c.String(nullable: false, maxLength: 20),
            //            HomeScore = c.Int(),
            //            AwayScore = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Team", t => t.AwayTeamId)
            //    .ForeignKey("dbo.Team", t => t.HomeTeamId)
            //    .ForeignKey("dbo.Tournament", t => t.TournamentId)
            //    .Index(t => t.TournamentId)
            //    .Index(t => t.HomeTeamId)
            //    .Index(t => t.AwayTeamId);
            
            //CreateTable(
            //    "dbo.Team",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            TournamentId = c.Int(nullable: false),
            //            Name = c.String(nullable: false, maxLength: 30),
            //            Group = c.String(nullable: false, maxLength: 10),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Tournament", t => t.TournamentId)
            //    .Index(t => t.TournamentId);
            
            //CreateTable(
            //    "dbo.Tournament",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 30),
            //            Place = c.String(nullable: false, maxLength: 30),
            //            StartDate = c.DateTime(nullable: false),
            //            EndDate = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.GuessGame",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            TournamentId = c.Int(nullable: false),
            //            Name = c.String(nullable: false, maxLength: 30),
            //            PasswordHash = c.Binary(nullable: false, maxLength: 64, fixedLength: true),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Tournament", t => t.TournamentId)
            //    .Index(t => t.TournamentId);
            
            //CreateTable(
            //    "dbo.User",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 30),
            //            PasswordHash = c.Binary(nullable: false, maxLength: 64, fixedLength: true),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Role", t => t.IdentityRole_Id)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(maxLength: 150),
                        ClaimValue = c.String(maxLength: 500),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.IdentityUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 500),
                        SecurityStamp = c.String(maxLength: 500),
                        PhoneNumber = c.String(maxLength: 50),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.UserGuessGame",
            //    c => new
            //        {
            //            GuessGameId = c.Int(nullable: false),
            //            UserId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.GuessGameId, t.UserId })
            //    .ForeignKey("dbo.GuessGame", t => t.GuessGameId, cascadeDelete: true)
            //    .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.GuessGameId)
            //    .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.UserLogin", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.UserClaim", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.UserRole", "IdentityRole_Id", "dbo.Role");
            //DropForeignKey("dbo.Team", "TournamentId", "dbo.Tournament");
            //DropForeignKey("dbo.Match", "TournamentId", "dbo.Tournament");
            //DropForeignKey("dbo.GuessGame", "TournamentId", "dbo.Tournament");
            //DropForeignKey("dbo.UserGuessGame", "UserId", "dbo.User");
            //DropForeignKey("dbo.UserGuessGame", "GuessGameId", "dbo.GuessGame");
            //DropForeignKey("dbo.Guess", "UserId", "dbo.User");
            //DropForeignKey("dbo.Match", "HomeTeamId", "dbo.Team");
            //DropForeignKey("dbo.Match", "AwayTeamId", "dbo.Team");
            //DropForeignKey("dbo.Guess", "MatchId", "dbo.Match");
            //DropIndex("dbo.UserGuessGame", new[] { "UserId" });
            //DropIndex("dbo.UserGuessGame", new[] { "GuessGameId" });
            DropIndex("dbo.UserLogin", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserClaim", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserRole", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserRole", new[] { "IdentityRole_Id" });
            //DropIndex("dbo.GuessGame", new[] { "TournamentId" });
            //DropIndex("dbo.Team", new[] { "TournamentId" });
            //DropIndex("dbo.Match", new[] { "AwayTeamId" });
            //DropIndex("dbo.Match", new[] { "HomeTeamId" });
            //DropIndex("dbo.Match", new[] { "TournamentId" });
            //DropIndex("dbo.Guess", new[] { "MatchId" });
            //DropIndex("dbo.Guess", new[] { "UserId" });
            //DropTable("dbo.UserGuessGame");
            DropTable("dbo.IdentityUsers");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.UserRole");
            DropTable("dbo.Role");
            //DropTable("dbo.User");
            //DropTable("dbo.GuessGame");
            //DropTable("dbo.Tournament");
            //DropTable("dbo.Team");
            //DropTable("dbo.Match");
            //DropTable("dbo.Guess");
        }
    }
}
