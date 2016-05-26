namespace Bll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUser : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Guess", "UserId", "dbo.User");
            //DropIndex("dbo.Guess", new[] { "UserId" });
            //DropIndex("dbo.UserGuessGame", new[] { "UserId" });
            //DropPrimaryKey("dbo.UserGuessGame");
            //AddColumn("dbo.Guess", "User_Id", c => c.String(maxLength: 128));
            //AlterColumn("dbo.UserGuessGame", "UserId", c => c.String(nullable: false, maxLength: 128));
            //AddPrimaryKey("dbo.UserGuessGame", new[] { "GuessGameId", "UserId" });
            //CreateIndex("dbo.Guess", "User_Id");
            //CreateIndex("dbo.UserGuessGame", "UserId");
            //AddForeignKey("dbo.Guess", "User_Id", "dbo.IdentityUsers", "Id");
            //DropTable("dbo.User");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.User",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 30),
            //            PasswordHash = c.Binary(nullable: false, maxLength: 64, fixedLength: true),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //DropForeignKey("dbo.Guess", "User_Id", "dbo.IdentityUsers");
            //DropIndex("dbo.UserGuessGame", new[] { "UserId" });
            //DropIndex("dbo.Guess", new[] { "User_Id" });
            //DropPrimaryKey("dbo.UserGuessGame");
            //AlterColumn("dbo.UserGuessGame", "UserId", c => c.Int(nullable: false));
            //DropColumn("dbo.Guess", "User_Id");
            //AddPrimaryKey("dbo.UserGuessGame", new[] { "GuessGameId", "UserId" });
            //CreateIndex("dbo.UserGuessGame", "UserId");
            //CreateIndex("dbo.Guess", "UserId");
            //AddForeignKey("dbo.Guess", "UserId", "dbo.User", "Id");
        }
    }
}
