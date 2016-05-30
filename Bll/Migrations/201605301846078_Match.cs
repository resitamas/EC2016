namespace Bll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Match : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Match", "isConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Match", "isConfirmed");
        }
    }
}
