namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Property", "Status", c => c.String(maxLength: 50));
            AlterColumn("dbo.Ad", "Price", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ad", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Property", "Status");
        }
    }
}
