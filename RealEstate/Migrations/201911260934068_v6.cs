namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Residential", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Residential", "Price", c => c.Int());
        }
    }
}
