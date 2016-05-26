namespace Bll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Drop : DbMigration
    {
        public override void Up()
        {

        }
        
        public override void Down()
        {
            DropColumn("dbo.IdentityUsers", "Discriminator");

        }
    }
}
