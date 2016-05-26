namespace Bll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kakas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IdentityUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.IdentityUsers", "Discriminator");
        }
    }
}
