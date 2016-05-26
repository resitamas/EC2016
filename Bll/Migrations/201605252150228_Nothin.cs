namespace Bll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nothin : DbMigration
    {
        public override void Up()
        {
            //DropIndex("dbo.Guess", new[] { "User_Id" });
            //DropColumn("dbo.Guess", "UserId");
            //RenameColumn(table: "dbo.Guess", name: "User_Id", newName: "UserId");
            //AlterColumn("dbo.Guess", "UserId", c => c.String(maxLength: 128));
            //CreateIndex("dbo.Guess", "UserId");
        }
        
        public override void Down()
        {
            //DropIndex("dbo.Guess", new[] { "UserId" });
            //AlterColumn("dbo.Guess", "UserId", c => c.Int(nullable: false));
            //RenameColumn(table: "dbo.Guess", name: "UserId", newName: "User_Id");
            //AddColumn("dbo.Guess", "UserId", c => c.Int(nullable: false));
            //CreateIndex("dbo.Guess", "User_Id");
        }
    }
}
